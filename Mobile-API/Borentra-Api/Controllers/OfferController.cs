namespace Borentra.API.Controllers
{
    using Borentra.API.Internal;
    using Borentra.API.Models;
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Web.Http;

    [RoutePrefix("Offer")]
    public class OfferController : AuthenticatedController
    {
        #region Members
        /// <summary>
        /// Item Core
        /// </summary>
        private readonly ItemCore itemCore = new ItemCore();

        /// <summary>
        /// Lucene Core
        /// </summary>
        private readonly LuceneCore luceneCore = new LuceneCore();
        #endregion

        #region Methods
        // GET <controller>/My
        [HttpGet]
        [Route("My")]
        public IHttpActionResult My(string token)
        {
            if (auth.IsNotValid(token))
            {
                return base.StatusCode(System.Net.HttpStatusCode.Unauthorized);
            }

            var callerId = auth.Device.UserIdentifier;

            var results = this.itemCore.Search(callerId, OfferType.Unknown, null, null, callerId);
            var offers = results.Select(r => Offer.Map(r));
            return this.Ok<IEnumerable<Offer>>(offers);
        }
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(string key, string token = null)
        {
            var authorized = auth.IsNotValid(token);

            if (string.IsNullOrWhiteSpace(key))
            {
                return this.BadRequest("key");
            }

            key = key.TrimIfNotNull();

            var callerId = authorized ? auth.Device.UserIdentifier : (Guid?)null;

            var item = this.itemCore.GetItem(null, key, callerId);
            if (null != item)
            {
                var offer = Offer.Map(item);
                return this.Ok<Offer>(offer);
            }
            else
            {
                return this.BadRequest("found nothing");
            }
        }
        [HttpGet]
        [Route("Search")]
        public IHttpActionResult Search(string token = null, string term = null, int limit = 50)
        {
            var authorized = auth.IsNotValid(token);
            
            var callerId = authorized ? auth.Device.UserIdentifier : (Guid?)null;

            var search = this.luceneCore.Search(term, callerId, limit, Reference.Item);
            var results = search.Select(s => Result.Map(s));
            return this.Ok<IEnumerable<Result>>(results);
        }
        [HttpPost]
        [Route("Save")]
        public IHttpActionResult Save(SecuredItem item)
        {
            if (auth.IsNotValid(item))
            {
                return base.StatusCode(System.Net.HttpStatusCode.Unauthorized);
            }

            if (null == item)
            {
                return this.BadRequest("item");
            }

            if (item.Delete && Guid.Empty == item.Identifier)
            {
                return this.BadRequest("delete must have identifier");
            }

            item.UserIdentifier = auth.Device.UserIdentifier;

            return this.Ok<Item>(this.itemCore.Save(item));
        }
        #endregion
    }
}