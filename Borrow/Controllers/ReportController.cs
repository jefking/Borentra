namespace Borentra.Controllers
{
    using Borentra.Core;
    using System.Web.Mvc;

    public class ReportController : Controller
    {
        #region Members
        /// <summary>
        /// Statistics Core
        /// </summary>
        private readonly StatisticsCore statsCore = new StatisticsCore();
        #endregion

        #region Methods
        //
        // GET: /Report/
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult MembersByCountry()
        {
            return View();
        }

        public ActionResult Leaderboard()
        {
            var callerId = User.IdentifierSafe();
            return View(this.statsCore.LeaderBoard(callerId, 4));
        }
        #endregion
    }
}