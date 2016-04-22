namespace Borentra.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Error Controller
    /// </summary>
    public class ErrorController : Controller
    {
        #region Methods
        //
        // GET: /Error/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Error/404
        public ActionResult Error404()
        {
            return View("404");
        }

        // GET: /Error/500
        public ActionResult Error500()
        {
            return View("500");
        }

        // GET: /Error/ErrorRegistration
        public ActionResult ErrorRegistration()
        {
            return View();
        }

        // GET: /Error/Registration
        public ActionResult Registration()
        {
            return this.RedirectToAction("ErrorRegistration");
        }
        #endregion
    }
}