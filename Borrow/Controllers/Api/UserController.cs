namespace Borentra.Controllers.Api
{
    using Borentra;
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    /// <summary>
    /// User Controller (API)
    /// </summary>
    public class UserController : ApiController
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
        #endregion

        #region Methods
        //
        // POST: /api/User/SaveProfile
        [Authorize]
        [HttpPost]
        public Profile SaveProfile(Profile profile)
        {
            if (null == profile)
            {
                throw new ArgumentNullException("profile");
            }

            profile.Identifier = User.Identifier();

            return profileCore.Save(profile);
        }

        [Authorize]
        [HttpDelete]
        public void Delete()
        {
            var userId = User.Identifier();

            profileCore.Delete(userId);
        }

        // POST: /api/User/My
        [Authorize]
        [HttpGet]
        public Profile My()
        {
            var userId = User.Identifier();

            return profileCore.SearchSingle(userId, null, userId);
        }

        //
        // GET: /api/User/Get
        public Profile Get(Guid? id)
        {
            var profile = new Profile()
            {
                Identifier = id.HasValue ? id.Value : User.Identifier(),
            };

            var callerId = User.IdentifierSafe();

            return profileCore.Search(profile, callerId);
        }

        // GET: /api/User/Search
        [HttpGet]
        public IEnumerable<Profile> Search(string s)
        {
            var callerId = User.IdentifierSafe();

            return profileCore.Search(s, null, callerId, false, 50, int.MaxValue);
        }
        #endregion
    }
}