namespace Borentra.API.Controllers
{
    using Borentra.API.Internal;
    using Borentra.API.Models;
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    [RoutePrefix("Conversation")]
    public class ConversationController : AuthenticatedController
    {
        #region Members
        /// <summary>
        /// Conversation Core
        /// </summary>
        private readonly ConversationCore conversation = new ConversationCore();
        #endregion

        #region Methods
        [Route("Save")]
        [HttpPost]
        public IHttpActionResult Save(SecuredComment comment)
        {
            if (auth.IsNotValid(comment))
            {
                return base.StatusCode(System.Net.HttpStatusCode.Unauthorized);
            }

            if (null == comment)
            {
                return this.BadRequest("comment");
            }

            if (string.IsNullOrWhiteSpace(comment.Body))
            {
                return this.BadRequest("body");
            }

            comment.FromUserIdentifier = auth.Device.UserIdentifier;

            return this.Ok<Comment>(conversation.Save(comment));
        }

        [Route("My")]
        [HttpGet]
        public IHttpActionResult My(string token)
        {
            if (auth.IsNotValid(token))
            {
                return base.StatusCode(System.Net.HttpStatusCode.Unauthorized);
            }

            var search = new ConversationSearch()
            {
                UserIdentifier = auth.Device.UserIdentifier,
            };

            return this.Ok<IEnumerable<Comment>>(conversation.Search(search));
        }

        [Route("Get")]
        [HttpGet]
        public IHttpActionResult Get(string token, Guid identifier)
        {
            if (auth.IsNotValid(token))
            {
                return base.StatusCode(System.Net.HttpStatusCode.Unauthorized);
            }

            if (Guid.Empty == identifier)
            {
                return this.BadRequest("identifier");
            }

            var search = new ConversationSearch()
            {
                UserIdentifier = auth.Device.UserIdentifier,
                Identifier = identifier
            };

            var results = conversation.Search(search);
            results = from r in results
                      orderby r.On
                      select r;

            return this.Ok<IEnumerable<Comment>>(results);
        }
        #endregion
    }
}