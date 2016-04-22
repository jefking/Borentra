namespace Borentra.Controllers
{
    using Borentra.Core;
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System;
    using System.Security.Claims;
    using System.Web.Mvc;
    using System.Web.Security;

    /// <summary>
    /// Account Controller
    /// </summary>
    public class AccountController : Controller
    {
        #region Members
        /// <summary>
        /// Profile Core
        /// </summary>
        private readonly ProfileCore profileCore = new ProfileCore();
        #endregion

        #region Methods
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return RedirectToActionPermanent("Index", "Home");
        }

        // Get: /Account/PeaceOut
        public ActionResult PeaceOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignUp()
        {
            return this.View();
        }

        //
        // POST: /Account/SignIn
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SignIn(FormCollection forms)
        {
            var userIdentity = User.Identity;

            if (userIdentity.IsAuthenticated)
            {
                var identity = userIdentity as ClaimsIdentity;

                var register = new RegisterModel()
                {
                    Email = identity.EmailAddress(),
                    NameIdentifier = identity.NameIdentifier(),
                    UserName = identity.Name,
                };

                var melissaCore = new MelissaCore();
                var IPAddress = melissaCore.GetClientIpAddress(this.Request);
                var user = Membership.GetUser(register.NameIdentifier);
                if (null == user)
                {
                    user = Membership.CreateUser(register.NameIdentifier, Guid.NewGuid().ToString(), register.Email);
                    
                    var profile = new UserSaveProfile()
                    {
                        UserIdentifier = user.Identifier(),
                        DisplayName = identity.Name(),
                        FacebookTokenExpiration = identity.Expiration(),
                        IdentityProvider = identity.IdentityProvider(),
                        FacebookAccessToken = identity.FacebookAccessToken(),
                        Email = register.Email,
                        FacebookId = long.Parse(register.NameIdentifier),
                        IpAddress = IPAddress,
                    };

                    var thread = this.profileCore.Register(profile.CallObject<ProfileFull>());
                    thread.Wait();

                    return RedirectToAction("Welcome", "Dashboard");
                }
                else
                {
                    var profile = new UserSaveProfile()
                    {
                        UserIdentifier = user.Identifier(),
                        FacebookTokenExpiration = identity.Expiration(),
                        FacebookAccessToken = identity.FacebookAccessToken(),
                        IpAddress = IPAddress,
                    };

                    profile.ExecuteNonQuery();

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("ErrorRegistration", "Error");
            }
        }
        #endregion
    }
}