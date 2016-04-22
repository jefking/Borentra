namespace Borentra.Controllers
{
    using Borentra.Core;
    using System;
    using System.Web.Mvc;

    public class CompanyController : Controller
    {
        #region Members
        /// <summary>
        /// Company Core
        /// </summary>
        private readonly CompanyCore companyCore = new CompanyCore();
        #endregion

        #region Methods
        [Authorize(Roles = "staff")]
        public ActionResult Unique(string key = null, Guid? id = null)
        {
            if (!id.HasValue && string.IsNullOrWhiteSpace(key))
            {
                return RedirectToAction("Index");
            }

            var data = companyCore.Get(key, id);
            if (null != data)
            {
                return this.View(data);
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        [Authorize(Roles = "staff")]
        public ActionResult Registration()
        {
            return this.View();
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Index(string email, string name)
        {
            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(name))
            {
                var userId = User.IdentifierSafe();
                companyCore.Queue(email, name, userId);
            }

            return this.Redirect("/");
        }
        #endregion
    }
}