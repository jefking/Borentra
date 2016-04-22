namespace Borentra.Controllers.Api
{
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    public class SearchController : ApiController
    {
        #region Members
        /// <summary>
        /// Bing Core
        /// </summary>
        private readonly BingCore bingCore = new BingCore();

        /// <summary>
        /// Profile Core
        /// </summary>
        private readonly ProfileCore profileCore = new ProfileCore();

        /// <summary>
        /// Lucene Core
        /// </summary>
        private readonly LuceneCore lucene = new LuceneCore();
        #endregion

        #region Methods
        [HttpGet]
        public IEnumerable<SearchResult> General(string term, int take = 10)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                throw new ArgumentException("term");
            }

            var userId = User.IdentifierSafe();

            return this.lucene.Search(term, userId, take);
        }

        [HttpGet]
        public IEnumerable<SearchResult> Profile(string term, int take = 10)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                throw new ArgumentException("term");
            }

            var userId = User.IdentifierSafe();

            return this.lucene.Search(term, userId, take, Reference.User);
        }

        [HttpGet]
        public IEnumerable<SearchResult> Offer(string term, int take = 10)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                throw new ArgumentException("term");
            }

            var userId = User.IdentifierSafe();

            return this.lucene.Search(term, userId, take, Reference.Item);
        }

        [HttpGet]
        [System.Web.Http.ActionName("Request")]
        public IEnumerable<SearchResult> ItemRequest(string term, int take = 10)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                throw new ArgumentException("term");
            }

            var userId = User.IdentifierSafe();

            return this.lucene.Search(term, userId, take, Reference.ItemRequest);
        }

        [Authorize(Roles = "staff")] //TEMP
        [HttpGet]
        public IEnumerable<SearchResult> Company(string term, int take = 10)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                throw new ArgumentException("term");
            }

            var userId = User.IdentifierSafe();

            return this.lucene.Search(term, userId, take, Reference.Company);
        }

        // GET:/Api/Search/Search?s=Query&take=10
        [Authorize]
        [HttpGet]
        public IEnumerable<ImageResult> PublicImages(string s, int take = 10)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException("s");
            }

            if (0 >= take || 50 < take)
            {
                take = 10;
            }

            var userId = User.Identifier();
            var profile = this.profileCore.SearchSingle(userId, null, userId);
            double? longitude = null;
            double? latitude = null;

            if (0 != profile.Longitude)
            {
                longitude = profile.Longitude;
            }

            if (0 != profile.Latitude)
            {
                latitude = profile.Latitude;
            }

            var images = bingCore.Search(s, longitude, latitude);
            return null == images ? null : images.Take(take);
        }
        #endregion
    }
}