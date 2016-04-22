namespace Borentra.Controllers
{
    using Borentra.Collections;
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    /// Requests Controller
    /// </summary>
    public class RequestsController : Controller
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

        /// <summary>
        /// Lucene Core
        /// </summary>
        private readonly LuceneCore luceneCore = new LuceneCore();
        #endregion

        #region Methods
        // GET:/Requests/
        public ActionResult Index(Guid? user, string s)
        {
            var callerId = User.IdentifierSafe();

            if (callerId.HasValue && user.HasValue && user.Value == callerId.Value)
            {
                return RedirectToAction("wanted", "dashboard");
            }

            const int MaxResults = 100;

            var results = new SearchResults<ItemRequest>()
            {
                SearchDisplayText = s,
                Manifest = itemRequestCore.Search(user, callerId, s, MaxResults),
            };
            
            return this.View(results);
        }

        // GET:/Requests/{Key}
        public ActionResult Unique(Guid? id = null, string key = null)
        {
            if (!id.HasValue && string.IsNullOrWhiteSpace(key))
            {
                return RedirectToAction("Index");
            }

            var callerId = User.IdentifierSafe();

            var master = new ItemRequestMaster()
            {
                Display = itemRequestCore.Get(key, id, callerId),
            };

            if (null != master.Display)
            {
                master.Display.SetCategories();
                var relatedProductQuery = string.Empty;
                if (null != master.Display.Categories
                    && 0 < master.Display.Categories.Count())
                {
                    relatedProductQuery = string.Join(" ", master.Display.Categories);
                    relatedProductQuery.Replace('#', (char)0);
                }
                else if (!string.IsNullOrWhiteSpace(master.Display.Title))
                {
                    relatedProductQuery = master.Display.Title;
                }

                if (!string.IsNullOrWhiteSpace(relatedProductQuery))
                {
                    var type = master.Display.IsMine ? Reference.Item : Reference.ItemRequest;
                    master.Results = from i in this.luceneCore.Search(relatedProductQuery, callerId, 6, type)
                                        where i.Key != key
                                        select i;
                }
            }
            else if (!string.IsNullOrWhiteSpace(key))
            {
                return Redirect(ItemRequestCore.SearchUrl(key, "unknown_key"));
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View(master);
        }
        #endregion
    }
}