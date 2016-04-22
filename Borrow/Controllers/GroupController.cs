namespace Borentra.Controllers
{
    using Borentra.Core;
    using Borentra.Models;
    using System.Web.Mvc;

    [Authorize]
    public class GroupController : Controller
    {
        #region Members
        /// <summary>
        /// Group Core
        /// </summary>
        private readonly GroupCore groupCore = new GroupCore();

        /// <summary>
        /// Profile Core
        /// </summary>
        private readonly ProfileCore profileCore = new ProfileCore();
        #endregion

        #region Methods
        public ActionResult Index()
        {
            var userId = User.Identifier();

            var profile = this.profileCore.SearchSingle<MyProfile>(userId, null, userId);
            ViewBag.Theme = profile.Theme;

            return this.View();
        }

        [HttpPost]
        public ActionResult Index(string email, string name)
        {
            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(name))
            {
                var userId = User.Identifier();
                groupCore.Queue(email, name, userId);
            }

            return this.Redirect("/");
        }
        #endregion
    }
}