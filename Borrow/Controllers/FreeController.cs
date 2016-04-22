namespace Borentra.Controllers
{
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Free Controller
    /// </summary>
    public class FreeController : Controller
    {
        #region Members
        /// <summary>
        /// Item Core
        /// </summary>
        private readonly ItemCore itemCore = new ItemCore();

        /// <summary>
        /// Item Request Core
        /// </summary>
        private readonly ItemRequestCore itemRequestCore = new ItemRequestCore();

        /// <summary>
        /// Profile Core
        /// </summary>
        private readonly ProfileCore profileCore = new ProfileCore();
        #endregion

        #region Methods
        //
        // GET: /Free/
        public ActionResult Index(Guid? user = null, string s = null, bool friends = false)
        {
            var callerId = User.IdentifierSafe();

            if (callerId.HasValue && user.HasValue && user.Value == callerId.Value)
            {
                return RedirectToAction("inventory", "dashboard");
            }

            var results = new SearchResults<Item>()
            {
                SearchDisplayText = "Free",
                Manifest = itemCore.Search(user, OfferType.Free, s, 100, callerId, friends),
            };
            
            if (user.HasValue && Guid.Empty != user.Value)
            {
                results.User = profileCore.SearchSingle(user, null, callerId);
            }

            return View("~/Views/Item/Index.cshtml", results);
        }
        #endregion
    }
}