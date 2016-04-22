namespace Borentra.API.Controllers
{
    using Borentra.API.Internal;
    using Borentra.Core;
    using Borentra.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    [RoutePrefix("Search")]
    public class SearchController : AuthenticatedController
    {
        #region Members
        /// <summary>
        /// Profile Core
        /// </summary>
        private readonly ProfileCore profileCore = new ProfileCore();

        /// <summary>
        /// Bing Core
        /// </summary>
        private readonly BingCore bingCore = new BingCore();
        #endregion

        #region Methods
        [HttpGet]
        [Route("PublicImages")]
        public IHttpActionResult PublicImages(string token, string s, int limit = 10)
        {
            if (auth.IsNotValid(token))
            {
                return base.StatusCode(System.Net.HttpStatusCode.Unauthorized);
            }

            if (string.IsNullOrWhiteSpace(s))
            {
                return base.BadRequest("no search term");
            }

            if (0 >= limit || 50 < limit)
            {
                limit = 10;
            }

            var userId = this.auth.Device.UserIdentifier;
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

            var images = this.bingCore.Search(s, longitude, latitude);
            var results = null == images ? null : images.Take(limit);
            return this.Ok<IEnumerable<ImageResult>>(results);
        }
        #endregion
    }
}