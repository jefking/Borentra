namespace Borentra.Controllers
{
    using Borentra.Core;
    using System.Web.Mvc;

    /// <summary>
    /// Help Controller
    /// </summary>
    public class HelpController : Controller
    {
        #region Members
        /// <summary>
        /// Badge Core
        /// </summary>
        private readonly BadgeCore badgeCore = new BadgeCore();
        #endregion

        #region Methods
        //
        // GET: /Help/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Help/FAQ
        public ActionResult FAQ()
        {
            return View();
        }

        //
        // GET: /Help/Badges
        public ActionResult Badges()
        {
            return View(this.badgeCore.Search());
        }
        #endregion
    }
}