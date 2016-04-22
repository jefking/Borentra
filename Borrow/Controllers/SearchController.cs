namespace Borentra.Controllers
{
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class SearchController : Controller
    {
        #region Members
        /// <summary>
        /// Lucene Core
        /// </summary>
        private readonly LuceneCore luceneCore = new LuceneCore();
        #endregion

        #region Methods
        //
        // GET: /Search/
        public ActionResult Index(string s = null)
        {
            var callerId = User.IdentifierSafe();

            return this.Search(s, callerId);
        }
        //
        // GET: /Search/
        public ActionResult Offer(string s = null)
        {
            var callerId = User.IdentifierSafe();

            return this.Search(s, callerId, Reference.Item);
        }
        //
        // GET: /Search/

        public ActionResult Member(string s = null)
        {
            var callerId = User.IdentifierSafe();

            return this.Search(s, callerId, Reference.User);
        }
        //
        // GET: /Search/

        public ActionResult Wanted(string s = null)
        {
            var callerId = User.IdentifierSafe();

            return this.Search(s, callerId, Reference.ItemRequest);
        }

        /// <summary>
        /// Search Helper, as everything is very general
        /// </summary>
        /// <param name="s"></param>
        /// <param name="callerId"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        private ActionResult Search(string s, Guid? callerId, Reference reference = Reference.None)
        {
            var results = luceneCore.Search(s, callerId, 100, reference);

            if (null != results && 1 == results.Count())
            {
                var item = results.First();
                return this.Redirect(item.RelativeLink());
            }
            else
            {
                return View("~/Views/Search/Index.cshtml", results);
            }
        }
        #endregion
    }
}