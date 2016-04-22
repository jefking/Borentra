namespace Borentra.Controllers.Api
{
    using Borentra.Core;
    using System;
    using System.Web.Http;

    public class GroupController : ApiController
    {
        #region Members
        private readonly GroupCore core = new GroupCore();
        #endregion

        #region Methods
        [HttpPost]
        public void Register(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("email");
            }

            var userId = User.IdentifierSafe();
            core.Queue(email, name, userId);
        }
        #endregion
    }
}