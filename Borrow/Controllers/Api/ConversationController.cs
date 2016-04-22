namespace Borentra.Controllers.Api
{
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    /// <summary>
    /// Converstation Controller
    /// </summary>
    [Authorize]
    public class ConversationController : ApiController
    {
        #region Members
        /// <summary>
        /// Conversation Core
        /// </summary>
        private readonly ConversationCore conversation = new ConversationCore();
        #endregion

        #region Methods
        [HttpPost]
        public Comment Save(Comment comment)
        {
            if (null == comment)
            {
                throw new ArgumentNullException("comment");
            }

            comment.FromUserIdentifier = User.Identifier();

            return conversation.Save(comment);
        }

        [HttpGet]
        public IEnumerable<Comment> Search(Guid? identifier = null)
        {
            var search = new ConversationSearch()
            {
                UserIdentifier = User.Identifier(),
                Identifier = identifier.HasValue ? identifier.Value : Guid.Empty,
            };

            var results = conversation.Search(search);
            if (identifier.HasValue)
            {
                results = from r in results
                          orderby r.On
                          select r;
            }

            return results;
        }

        [HttpGet]
        public IEnumerable<ItemActionComment> ItemActionComments(Guid id)
        {
            if (Guid.Empty == id)
            {
                throw new ArgumentException("id");
            }

            var userId = User.Identifier();

            return this.conversation.ItemActionComments(userId, id);
        }
        #endregion
    }
}