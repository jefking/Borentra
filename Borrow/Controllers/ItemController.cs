namespace Borentra.Controllers
{
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    /// Item Controller
    /// </summary>
    public class ItemController : Controller
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
        /// Borrow Core
        /// </summary>
        private readonly BorrowCore borrowCore = new BorrowCore();

        /// <summary>
        /// Lucene Core
        /// </summary>
        private readonly LuceneCore luceneCore = new LuceneCore();

        /// <summary>
        /// Good Reads Core
        /// </summary>
        private readonly GoodReadsCore goodReadsCore = new GoodReadsCore();
        #endregion

        #region Methods
        //
        // GET: /item/{Key}
        public ActionResult Unique(Guid? id = null, string key = null)
        {
            if (!id.HasValue && string.IsNullOrWhiteSpace(key))
            {
                return RedirectToAction("Index");
            }

            var callerId = User.IdentifierSafe();

            var master = new ItemMaster()
            {
                Display = itemCore.GetItem(id, key, callerId),
            };

            if (null != master.Display)
            {
                master.Display.SetCategories();
                var dic = new Dictionary<Guid, ItemShare>();
                foreach (var s in from s in borrowCore.Shares(master.Display)
                                  where s.Status == BorrowStatus.Returned
                                  select s)
                {
                    if (!dic.ContainsKey(s.RequesterUserIdentifier))
                    {
                        dic.Add(s.RequesterUserIdentifier, s);
                    }
                }
                master.Display.Shares = dic.Values;

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
                    master.Results = from i in this.luceneCore.Search(relatedProductQuery, callerId, 6, Reference.Item)
                                      where i.Key != key
                                      select i;
                }

                if (null != master.Display.Categories
                    && (from c in master.Display.Categories where c.ToLower() == "book" || c.ToLower() == "books" select c).Count() > 0)
                {
                    master.Display.ExternalReviews = this.goodReadsCore.GetReviewToken(master.Display.Title);
                }

                return View(master);
            }
            else if (!string.IsNullOrWhiteSpace(key))
            {
                return Redirect(ItemCore.SearchUrl(key, "unknown_key"));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /item
        public ActionResult Index(Guid? user = null, string s = null, bool friends = false)
        {
            var callerId = User.IdentifierSafe();

            if (callerId.HasValue && user.HasValue && user.Value == callerId.Value)
            {
                return RedirectToAction("inventory", "dashboard");
            }

            var results = new SearchResults<Item>()
            {
                Manifest = itemCore.Search(user, s, null, 100, callerId, false, friends),
                SearchDisplayText = s,
            };

            if (user.HasValue && Guid.Empty != user.Value)
            {
                results.User = profileCore.SearchSingle(user, null, callerId);
            }

            if (!string.IsNullOrWhiteSpace(s))
            {
                switch (s)
                {
                    case "share":
                        results.SearchDisplayText = "Lend";
                        break;
                    case "free":
                        results.SearchDisplayText = "Give away";
                        break;
                    case "rent":
                        results.SearchDisplayText = "Rent";
                        break;
                    case "trade":
                        results.SearchDisplayText = "Trade";
                        break;
                }
            }

            return View(results);
        }
        #endregion
    }
}