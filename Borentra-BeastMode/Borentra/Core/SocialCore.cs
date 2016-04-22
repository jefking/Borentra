namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Social Core
    /// </summary>
    public class SocialCore
    {
        #region Methods
        public SocialFavorite SaveFavorite(SocialFavorite favorite)
        {
            var sproc = new SocialSaveFavorite()
            {
                UserIdentifier = favorite.UserIdentifier,
                ReferenceIdentifier = favorite.ReferenceIdentifier,
                Delete = favorite.Delete
            };

            return sproc.CallObject<SocialFavorite>();
        }

        public IEnumerable<SocialFavorite> SearchFavorite(Guid referenceIdentifier)
        {
            var sproc = new SocialSearchFavorite()
            {
                ReferenceIdentifier = referenceIdentifier,
            };

            return sproc.CallObjects<SocialFavorite>();
        }

        public SocialRating SaveRating(SocialRating rating)
        {
            var sproc = new SocialSaveRating()
            {
                UserIdentifier = rating.UserIdentifier,
                ReferenceIdentifier = rating.ReferenceIdentifier,
                Rating = rating.Rating,
                Delete = rating.Delete,
            };

            return sproc.CallObject<SocialRating>();
        }

        public IEnumerable<SocialRating> SearchRating(Guid userIdentifier, Guid referenceIdentifier)
        {
            var sproc = new SocialSearchRating()
            {
                UserIdentifier = userIdentifier,
                ReferenceIdentifier = referenceIdentifier,
            };

            return sproc.CallObjects<SocialRating>();
        }

        public SocialComment SaveComment(SocialComment comment)
        {
            var identifier = comment.Identifier == Guid.Empty ? (Guid?)null : comment.Identifier;

            var sproc = new SocialSaveComment()
            {
                Identifier = identifier,
                UserIdentifier = comment.UserIdentifier,
                ReferenceIdentifier = comment.ReferenceIdentifier,
                Comment = comment.Comment,
                Delete = comment.Delete,
            };

            return sproc.CallObject<SocialComment>();
        }

        public IEnumerable<SocialComment> SearchComment(Guid callerIdentifier, Guid referenceIdentifier)
        {
            var sproc = new SocialSearchComment()
            {
                CallerIdentifier = callerIdentifier,
                ReferenceIdentifier = referenceIdentifier,
            };

            return sproc.CallObjects<SocialComment>();
        }

        public void SaveTags(SocialTags tags)
        {
            if (null == tags)
            {
                throw new ArgumentNullException("tags");
            }

            var sproc = new SocialSaveTags()
            {
                ReferenceIdentifier = tags.ReferenceIdentifier,
                Tags = tags.Tags,
                UserIdentifier = tags.UserIdentifier,
            };

            sproc.Execute();
        }

        /// <summary>
        /// Notifications
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Notifications Notifications(Guid userId)
        {
            if (Guid.Empty == userId)
            {
                throw new ArgumentException("userId");
            }

            var sproc = new SocialUserNotifications()
            {
                UserIdentifier = userId,
            };

            return sproc.CallObject<Notifications>();
        }

        public void SaveContacts(IEnumerable<MobileContact> contacts)
        {
            if (null != contacts)
            {
                foreach (var contact in contacts)
                {
                    if (Guid.Empty != contact.UserIdentifier && !string.IsNullOrWhiteSpace(contact.Email))
                    {
                        try
                        {
                            this.SaveContact(contact);
                        }
                        catch
                        {
                            //Safety, need logging
                        }
                    }
                }
            }
        }
        public void SaveContact(MobileContact contact)
        {
            var sproc = new SocialSaveContact()
            {
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                UserIdentifier = contact.UserIdentifier,
                PhoneNumber = contact.PhoneNumber,
                Invite = contact.Invite,
            };

            sproc.ExecuteNonQuery();
        }
        #endregion
    }
}