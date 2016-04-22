namespace Borentra.Controllers
{
    using Borentra.Core;
    using Borentra.Models;
    using Borentra.Web;
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Rss Feed for Data
    /// </summary>
    public class RssController : Controller
    {
        #region Members
        /// <summary>
        /// Item Core
        /// </summary>
        private readonly ItemCore itemCore = new ItemCore();

        /// <summary>
        /// Title Format
        /// </summary>
        private const string titleFormat = "Borentra: {0}";

        /// <summary>
        /// Description Format
        /// </summary>
        private const string descriptionFormat = "Share more with your friends & local community. {0} from our community";
        #endregion

        #region Methods
        /// <summary>
        /// Items & Wanted
        /// </summary>
        /// <returns>RSS Data</returns>
        public ActionResult Index()
        {
            const string description = "Offers & Wanted";

            var items = itemCore.Rss<ItemRss>();
            if (null != items)
            {
                return new RssResult(items
                    , string.Format(titleFormat, description)
                    , string.Format(descriptionFormat, description));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        // DEPRECATED
        public ActionResult Items()
        {
            return this.Index();
        }

        /// <summary>
        /// Items
        /// </summary>
        /// <returns>RSS Data</returns>
        public ActionResult Item(bool withImages = false)
        {
            return this.Offer(withImages);
        }
        public ActionResult Offer(bool withImages = false)
        {
            const string description = "Offers";

            var items = itemCore.Rss<ItemRss>(1, withImages);
            if (null != items)
            {
                return new RssResult(items
                    , string.Format(titleFormat, description)
                    , string.Format(descriptionFormat, description));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        /// <summary>
        /// Wanted
        /// </summary>
        /// <returns>RSS Data</returns>
        public ActionResult Wanted(bool withImages = false)
        {
            const string description = "Wanted";

            var items = itemCore.Rss<ItemRss>(2, withImages);
            if (null != items)
            {
                return new RssResult(items
                    , string.Format(titleFormat, description)
                    , string.Format(descriptionFormat, description));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        #endregion
    }
}