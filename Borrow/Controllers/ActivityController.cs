namespace Borentra.Controllers
{
    using System;
    using System.Web.Mvc;

    [Authorize]
    public class ActivityController : Controller
    {
        #region Methods
        //
        // GET: /Activity/
        public ActionResult Index(Guid activityId)
        {
            return View(activityId);
        }
        #endregion
    }
}