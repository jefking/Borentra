namespace Borentra.API.Controllers
{
    using Borentra.API.Internal;
    using Borentra.API.Models;
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Http;

    [RoutePrefix("Want")]
    public class WantController : AuthenticatedController
    {
        #region Members
        /// <summary>
        /// Item Request Core
        /// </summary>
        private readonly ItemRequestCore wantCore = new ItemRequestCore();

        /// <summary>
        /// Lucene Core
        /// </summary>
        private readonly LuceneCore luceneCore = new LuceneCore();

        /// <summary>
        /// Image Core
        /// </summary>
        private readonly ImageCore imageCore = new ImageCore();
        #endregion

        #region Methods
        [HttpGet]
        [Route("My")]
        public IHttpActionResult My(string token)
        {
            if (auth.IsNotValid(token))
            {
                return base.StatusCode(System.Net.HttpStatusCode.Unauthorized);
            }

            var callerId = auth.Device.UserIdentifier;
            var requests = this.wantCore.Search(callerId, callerId);
            var wants = requests.Select(r => Want.Map(r));
            return this.Ok<IEnumerable<Want>>(wants);
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
            var itemRequest = this.wantCore.Get(key, null, callerId);
            if (null != itemRequest)
            {
                var want = Want.Map(itemRequest);
                return this.Ok<Want>(want);
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

            var search = this.luceneCore.Search(term, callerId, limit, Reference.ItemRequest);
            var results = search.Select(s => Result.Map(s));
            return this.Ok<IEnumerable<Result>>(results);
        }
        [HttpPost]
        [Route("Save")]
        public IHttpActionResult Save(SecuredWant want)
        {
            if (auth.IsNotValid(want))
            {
                return base.StatusCode(System.Net.HttpStatusCode.Unauthorized);
            }

            if (null == want)
            {
                return this.BadRequest("want");
            }

            if (want.Delete && Guid.Empty == want.Identifier)
            {
                return this.BadRequest("delete must have identifier");
            }

            if (Guid.Empty == want.Identifier)
            {
                want.Identifier = Guid.NewGuid();
            }
            if (!string.IsNullOrWhiteSpace(want.ImageUrl))
            {
                try
                {
                    this.SaveImagebyUrl(this.auth.Device.UserIdentifier, want.Identifier, want.ImageUrl);
                }
                catch { }
            }
            want.UserIdentifier = auth.Device.UserIdentifier;

            var itemRequest = this.wantCore.Save(want);
            if (null != itemRequest)
            {
                var result = Want.Map(itemRequest);
                return this.Ok<Want>(result);
            }
            else
            {
                return this.Ok();
            }
        }
        public void SaveImagebyUrl(Guid userId, Guid wantId, string url)
        {
            if (Guid.Empty != userId && Guid.Empty != wantId && !String.IsNullOrWhiteSpace(url))
            {
                var itemImage = new ItemRequestImageInput()
                {
                    UserIdentifier = userId,
                    ItemRequestIdentifier = wantId,
                    FileName = url,
                };

                using (var file = imageCore.Download(url))
                {
                    itemImage.ContentType = file.ContentType;
                    using (var response = file.GetResponseStream())
                    {
                        using (var ms = new MemoryStream())
                        {
                            response.CopyTo(ms);
                            itemImage.Contents = ms.ToArray();
                        }
                    }
                }

                var result = this.imageCore.Save(itemImage);
            }
        }
        #endregion
    }
}