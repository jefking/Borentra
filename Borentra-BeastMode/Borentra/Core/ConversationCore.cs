namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Converstation Core
    /// </summary>
    public class ConversationCore
    {
        #region Members
        /// <summary>
        /// Email Core
        /// </summary>
        private readonly EmailCore email = new EmailCore();

        /// <summary>
        /// Profile Core
        /// </summary>
        private readonly ProfileCore profileCore = new ProfileCore();
        #endregion

        #region Methods
        /// <summary>
        /// Save Comment
        /// </summary>
        /// <param name="comment">Comment</param>
        /// <returns>Comment</returns>
        public Comment Save(Comment comment)
        {
            if (null == comment)
            {
                throw new ArgumentNullException("search");
            }

            var proc = new SocialSaveConversation()
            {
                Comment = comment.Body,
                Read = comment.Read,
                ParentConversationIdentifier = comment.ParentConversationIdentifier == Guid.Empty ? (Guid?)null : comment.ParentConversationIdentifier,
                ToUserIdentifier = comment.ToUserIdentifier == Guid.Empty ? (Guid?)null : comment.ToUserIdentifier,
                UserIdentifier = comment.FromUserIdentifier,
                Identifier = comment.Identifier == Guid.Empty ? (Guid?)null : comment.Identifier,
            };

            var data = proc.CallObject<Comment>();
            if (!comment.Read && null != data && Guid.Empty != data.ToUserIdentifier)
            {
                var user = new ProfileFull()
                {
                    Identifier = data.ToUserIdentifier,
                };

                user = profileCore.Search<ProfileFull>(user, null, true);
                email.Conversation(data, user);
            }

            return data;
        }

        public void NewUserGreeting(Guid userId, string name)
        {
            const string bodyFormat = @"Hi {0},

Welcome to the Borentra community, please let me know if there is anything I can do to help you get Sharing!";

            var comment = new Comment()
            {
                FromUserIdentifier = new Guid("35833228-2B5D-4961-963C-8D682FACFD0E"), // Jef King
                ToUserIdentifier = userId,
                Body = string.Format(bodyFormat, name.FirstPart()),
            };

            this.Save(comment);
        }

        public IEnumerable<Comment> Search(ConversationSearch search)
        {
            if (null == search)
            {
                throw new ArgumentNullException("search");
            }

            var proc = new SocialSearchConversation()
            {
                Identifier = search.Identifier.ToNullable(),
                UserIdentifier = search.UserIdentifier,
            };

            return proc.CallObjects<Comment>();
        }

        /// <summary>
        /// Item Action Comments
        /// </summary>
        /// <param name="userIdentifier">User Identifier</param>
        /// <param name="id">Identifier</param>
        /// <returns>Item Action Comments</returns>
        public IEnumerable<ItemActionComment> ItemActionComments(Guid userIdentifier, Guid id)
        {
            var sproc = new GoodsSearchItemActionComment()
            {
                ItemActionIdentifier = id,
                UserIdentifier = userIdentifier,
            };

            return sproc.CallObjects<ItemActionComment>();
        }
        #endregion
    }
}