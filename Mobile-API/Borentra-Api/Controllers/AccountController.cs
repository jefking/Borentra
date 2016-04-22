namespace Borentra.API.Controllers
{
    using Borentra.API.Internal;
    using Borentra.API.Models;
    using Borentra.Core;
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System;
    using System.Web.Http;
    using System.Web.Security;

    [RoutePrefix("Account")]
    public class AccountController : AuthenticatedController
    {
        #region Members
        /// <summary>
        /// Profile Core
        /// </summary>
        private readonly ProfileCore profileCore = new ProfileCore();

        /// <summary>
        /// Melissa Core
        /// </summary>
        private readonly MelissaCore melissaCore = new MelissaCore();

        /// <summary>
        /// Facebook Core
        /// </summary>
        private readonly FacebookCore facebookCore = new FacebookCore();

        /// <summary>
        /// Device Core
        /// </summary>
        private readonly DeviceCore deviceCore = new DeviceCore();
        #endregion

        #region Methods
        [Route("Valid")]
        [HttpGet]
        public IHttpActionResult Validate(string token)
        {
            if (this.auth.IsNotValid(token))
            {
                return base.StatusCode(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                return this.Ok();
            }
        }
        // POST Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout(Token token)
        {
            if (null == token)
            {
                return this.BadRequest("token");
            }

            var st = token.ToSecuredToken();
            if (null == st)
            {
                return this.BadRequest("secured token");
            }

            this.deviceCore.LogOut(st.Id);

            return Ok();
        }

        // POST Account/Login
        [Route("Login")]
        public IHttpActionResult Login(Registration registration)
        {
            if (null == registration)
            {
                return this.BadRequest("no data");
            }

            if (string.IsNullOrWhiteSpace(registration.FacebookAccessToken))
            {
                return this.BadRequest("facebook access token");
            }

            if (Guid.Empty == registration.DeviceIdentifier)
            {
                return this.BadRequest("device identifier");
            }

            if (registration.FacebookTokenExpiration < DateTime.UtcNow)
            {
                registration.FacebookTokenExpiration = DateTime.UtcNow.AddHours(2);
            }

            var registered = new Registered()
            {
                Registration = registration,
                IpAddress = this.melissaCore.GetClientIpAddress(base.Request.Properties),
                FacebookInfo = this.facebookCore.UserInfoSafe(registration),
            };

            if (registered.IsFacebookVerified)
            {
                var user = Membership.GetUser(registered.UserKey);
                if (null == user)
                {
                    if (string.IsNullOrWhiteSpace(registered.FacebookInfo.Name))
                    {
                        return BadRequest("not verified");
                    }

                    if (string.IsNullOrWhiteSpace(registered.FacebookInfo.Email))
                    {
                        return BadRequest("not verified");
                    }

                    user = Membership.CreateUser(registered.UserKey, Guid.NewGuid().ToString(), registered.FacebookInfo.Email);

                    registered.UserIdentifier = user.Identifier();

                    var profile = new Profile()
                    {
                        Identifier = registered.UserIdentifier,
                        Email = registered.FacebookInfo.Email,
                        Name = registered.FacebookInfo.Name,
                        FacebookId = registered.FacebookInfo.FacebookId,
                        IpAddress = registered.IpAddress,
                    };

                    profile = this.profileCore.Save(profile, true, registered.FacebookInfo.FacebookAccessToken);

                    var full = this.profileCore.SearchSingle<ProfileFull>(profile.Identifier, null, profile.Identifier);
                    this.profileCore.Register(full);
                }
                else
                {
                    registered.UserIdentifier = user.Identifier();
                }

                var device = new Device()
                {
                    UserIdentifier = registered.UserIdentifier,
                    DeviceIdentifier = registered.Registration.DeviceIdentifier,
                    IpAddress = registered.IpAddress,
                    OperatingSystem = registered.Registration.OperatingSystem,
                    FacebookId = registered.FacebookInfo.FacebookId,
                    FacebookAccessToken = registered.FacebookInfo.FacebookAccessToken,
                    FacebookTokenExpiration = registered.FacebookInfo.FacebookTokenExpiration,
                    FacebookIsValidated = registered.IsFacebookVerified,
                    KeyExpiresOn = DateTime.UtcNow.AddHours(3),
                };

                device = this.deviceCore.Save(device);

                return this.Ok<IToken>(device.ToToken());
            }

            return BadRequest("not verified");
        }
        #endregion
    }
}