namespace Borentra.API.Controllers
{
    using Borentra.API.Internal;
    using Borentra.API.Models;
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Http;

    [RoutePrefix("Social")]
    public class SocialController : AuthenticatedController
    {
        #region Members
        /// <summary>
        /// Activity Core
        /// </summary>
        private readonly ActivityCore activityCore = new ActivityCore();

        /// <summary>
        /// Social Core
        /// </summary>
        private readonly SocialCore socialCore = new SocialCore();

        /// <summary>
        /// Email Core
        /// </summary>
        private readonly EmailCore emailCore = new EmailCore();
        #endregion

        #region Methods
        [HttpGet]
        [Route("SearchActivity")]
        public IHttpActionResult SearchActivity(string token, byte limit = 20)
        {
            if (auth.IsNotValid(token))
            {
                return base.StatusCode(HttpStatusCode.Unauthorized);
            }

            var results = this.activityCore.Search(auth.Device.UserIdentifier, limit);
            if (null != results && 0 < results.Count())
            {
                var activities = results.Select(r => ApiActivity.Map(r));
                return this.Ok<IEnumerable<ApiActivity>>(activities);
            }
            else
            {
                return this.BadRequest("found nothing");
            }
        }

        [HttpGet]
        [Route("MyActivity")]
        public IHttpActionResult MyActivity(string token, byte limit = 10)
        {
            if (auth.IsNotValid(token))
            {
                return base.StatusCode(HttpStatusCode.Unauthorized);
            }

            var results = this.activityCore.UserSearch(auth.Device.UserIdentifier, limit);

            if (null != results && 0 < results.Count())
            {
                var activities = results.Select(r => ApiActivity.Map(r));
                return this.Ok<IEnumerable<ApiActivity>>(activities);
            }
            else
            {
                return this.BadRequest("found nothing");

            }
        }
        [HttpPost]
        [Route("SaveFavorite")]
        public IHttpActionResult SaveFavorite(SecuredSocialFavorite favorite)
        {
            if (auth.IsNotValid(favorite))
            {
                return base.StatusCode(HttpStatusCode.Unauthorized);
            }

            if (null == favorite)
            {
                return this.BadRequest("Favorite");
            }

            if (Guid.Empty == favorite.ReferenceIdentifier)
            {
                return this.BadRequest("Reference Identifier");
            }

            favorite.UserIdentifier = auth.Device.UserIdentifier;
            var saved = this.socialCore.SaveFavorite(favorite);

            var result = null != saved ? this.socialCore.SearchFavorite(saved.ReferenceIdentifier) : null;
            return this.Ok<IEnumerable<SocialFavorite>>(result);
        }
        [HttpPost]
        [Route("SaveComment")]
        public IHttpActionResult SaveComment(SecuredSocialComment comment)
        {
            if (auth.IsNotValid(comment))
            {
                return base.StatusCode(HttpStatusCode.Unauthorized);
            }

            if (null == comment)
            {
                return this.BadRequest("comment");
            }

            if (comment.Delete)
            {
                if (Guid.Empty == comment.Identifier)
                {
                    return this.BadRequest("Identifier");
                }
            }
            else
            {
                if (Guid.Empty == comment.ReferenceIdentifier)
                {
                    return this.BadRequest("Reference Identifier");
                }

                if (string.IsNullOrWhiteSpace(comment.Comment))
                {
                    return this.BadRequest("Comment must be specified");
                }
            }

            comment.UserIdentifier = auth.Device.UserIdentifier;
            var newComment = this.socialCore.SaveComment(comment);

            emailCore.NewsFeedComment(newComment);

            var results = null != newComment ? this.socialCore.SearchComment(comment.UserIdentifier, newComment.ReferenceIdentifier) : null;
            var comments = results.Select(r => ApiComment.Map(r));
            return this.Ok<IEnumerable<ApiComment>>(comments);
        }
        [HttpGet]
        [Route("Comments")]
        public IHttpActionResult Comments(string token, Guid referenceId)
        {
            if (auth.IsNotValid(token))
            {
                return base.StatusCode(System.Net.HttpStatusCode.Unauthorized);
            }

            if (Guid.Empty == referenceId)
            {
                return this.BadRequest("Reference Identifier");
            }

            var userId = auth.Device.UserIdentifier;
            var results = this.socialCore.SearchComment(userId, referenceId);
            var comments = results.Select(r => ApiComment.Map(r));
            return this.Ok<IEnumerable<ApiComment>>(comments);
        }
        [HttpPost]
        [Route("SaveContacts")]
        public IHttpActionResult SaveContacts(MobileContacts contacts)
        {
            if (auth.IsNotValid(contacts))
            {
                return base.StatusCode(HttpStatusCode.Unauthorized);
            }

            if (null == contacts.Contacts)
            {
                return this.BadRequest("no contacts");
            }

            var userId = auth.Device.UserIdentifier;
            foreach (var contact in contacts.Contacts)
            {
                contact.UserIdentifier = userId;
            }

            this.socialCore.SaveContacts(contacts.Contacts);

            return this.Ok();
        }
        #endregion
    }
}