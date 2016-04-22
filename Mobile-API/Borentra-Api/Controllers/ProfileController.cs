namespace Borentra.API.Controllers
{
    using Borentra.API.Internal;
    using Borentra.API.Models;
    using Borentra.Core;
    using System;
    using System.Web.Http;

    [RoutePrefix("Profile")]
    public class ProfileController : AuthenticatedController
    {
        #region Members
        /// <summary>
        /// Profile Core
        /// </summary>
        private readonly ProfileCore profileCore = new ProfileCore();
        #endregion

        #region Methods
        // GET <controller>/My
        [HttpGet]
        [Route("My")]
        public IHttpActionResult My(string token)
        {
            if (auth.IsNotValid(token))
            {
                return base.StatusCode(System.Net.HttpStatusCode.Unauthorized);
            }

            var userId = auth.Device.UserIdentifier;

            var profile = this.profileCore.SearchSingle(userId, null, userId);
            if (null != profile)
            {
                var member = Member.Map(profile);
                return this.Ok<Member>(member);
            }
            else
            {
                return this.BadRequest("found nothing");
            }
        }

        // GET <controller>/Get?key={0}
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(string key, string token = null)
        {
            var authorized = auth.IsNotValid(token);

            if (string.IsNullOrWhiteSpace(key))
            {
                return this.BadRequest("key");
            }

            key = key.TrimIfNotNull();

            var callerId = authorized ? auth.Device.UserIdentifier : (Guid?)null;

            var profile = this.profileCore.SearchSingle(null, key, callerId);
            if (null != profile)
            {
                var member = Member.Map(profile);
                return this.Ok<Member>(member);
            }
            else
            {
                return this.BadRequest("found nothing");
            }
        }
        [HttpPost]
        [Route("Save")]
        public IHttpActionResult Save(SecuredProfile profile)
        {
            if (auth.IsNotValid(profile))
            {
                return base.StatusCode(System.Net.HttpStatusCode.Unauthorized);
            }

            if (null == profile)
            {
                return this.BadRequest("profile");
            }

            profile.Identifier = auth.Device.UserIdentifier;

            var result = this.profileCore.Save(profile, true, null, auth.Device.OperatingSystem);
            if (null != result)
            {
                var member = Member.Map(result);
                return this.Ok<Member>(member);
            }
            else
            {
                return this.BadRequest("nothing returned");
            }
        }
        #endregion
    }
}