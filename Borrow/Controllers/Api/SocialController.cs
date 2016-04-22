namespace Borentra.Controllers.Api
{
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    [Authorize]
    public class SocialController : ApiController
    {
        #region Members

        private readonly ActivityCore activityCore = new ActivityCore();
        private readonly SocialCore socialCore = new SocialCore();
        private readonly EmailCore emailCore = new EmailCore();

        #endregion

        #region Methods
        // GET:/API/Social/SearchActivity
        [HttpGet]
        public IEnumerable<Activity> SearchActivity(byte limit = 20)
        {
            var userId = User.Identifier();
            return activityCore.Search(userId, limit);
        }

        // GET:/API/Social/MyActivity
        [HttpGet]
        public IEnumerable<Activity> MyActivity(byte limit = 10)
        {
            var userId = User.Identifier();
            return activityCore.UserSearch(userId, limit);
        }

        [HttpGet]
        public Notifications Notifications()
        {
            var userId = User.Identifier();
            return this.socialCore.Notifications(userId);
        }

        // POST:/API/Social/SaveFavorite
        [HttpPost]
        public IEnumerable<SocialFavorite> SaveFavorite(SocialFavorite favorite)
        {
            if (null == favorite)
            {
                throw new ArgumentNullException("Favorite");
            }

            if (Guid.Empty == favorite.ReferenceIdentifier)
            {
                throw new ArgumentException("Reference Identifier");
            }

            favorite.UserIdentifier = User.Identifier();
            var saved = this.socialCore.SaveFavorite(favorite);

            return null != saved ? this.socialCore.SearchFavorite(saved.ReferenceIdentifier) : null;
        }

        // GET:/API/Social/Favorite
        [HttpGet]
        public IEnumerable<SocialFavorite> Favorites(Guid referenceId)
        {
            if (Guid.Empty == referenceId)
            {
                throw new ArgumentException("Reference Identifier");
            }

            var userId = User.Identifier();
            return this.socialCore.SearchFavorite(referenceId);
        }

        // POST:/API/Social/SaveRating
        [HttpPost]
        public SocialRating SaveRating(SocialRating rating)
        {
            if (null == rating)
            {
                throw new ArgumentNullException("Rating");
            }

            if (Guid.Empty == rating.ReferenceIdentifier)
            {
                throw new ArgumentException("Reference Identifier");
            }

            if (0 > rating.Rating || 5 < rating.Rating)
            {
                throw new ArgumentException("Rating is invalid.");
            }

            rating.UserIdentifier = User.Identifier();
            return this.socialCore.SaveRating(rating);
        }

        // GET:/API/Social/Ratings
        [HttpGet]
        public IEnumerable<SocialRating> Ratings(Guid referenceId)
        {
            if (Guid.Empty == referenceId)
            {
                throw new ArgumentException("Reference Identifier");
            }

            var userId = User.Identifier();
            return this.socialCore.SearchRating(userId, referenceId);
        }

        // POST:/API/Social/SaveComment
        [HttpPost]
        public IEnumerable<SocialComment> SaveComment(SocialComment comment)
        {
            if (null == comment)
            {
                throw new ArgumentNullException("comment");
            }

            if (comment.Delete)
            {
                if (Guid.Empty == comment.Identifier)
                {
                    throw new ArgumentException("Identifier");
                }
            }
            else
            {
                if (Guid.Empty == comment.ReferenceIdentifier)
                {
                    throw new ArgumentException("Reference Identifier");
                }

                if (string.IsNullOrWhiteSpace(comment.Comment))
                {
                    throw new ArgumentException("Comment must be specified");
                }
            }
            comment.UserIdentifier = User.Identifier();
            var newComment = this.socialCore.SaveComment(comment);

            emailCore.NewsFeedComment(newComment);

            return null != newComment ? this.socialCore.SearchComment(comment.UserIdentifier, newComment.ReferenceIdentifier) : null;
        }

        // GET:/API/Social/Comments
        [HttpGet]
        public IEnumerable<SocialComment> Comments(Guid referenceId)
        {
            if (Guid.Empty == referenceId)
            {
                throw new ArgumentException("Reference Identifier");
            }

            var userId = User.Identifier();
            return this.socialCore.SearchComment(userId, referenceId);
        }

        // POST:/API/Social/SaveTags
        [HttpPost]
        public void SaveTags(SocialTags tags)
        {
            if (null == tags)
            {
                throw new ArgumentNullException("tags");
            }

            if (Guid.Empty == tags.ReferenceIdentifier)
            {
                throw new ArgumentException("Reference Identifier");
            }

            if (string.IsNullOrWhiteSpace(tags.Tags))
            {
                throw new ArgumentException("Tags must be specified");
            }
            tags.UserIdentifier = User.Identifier();
            this.socialCore.SaveTags(tags);
        }

        [HttpPost]
        public void SaveActivity(string activity)
        {
            if (string.IsNullOrWhiteSpace(activity))
            {
                throw new ArgumentException("activity");
            }

            var userId = User.Identifier();
            this.activityCore.StatusUpdate(userId, activity);
        }
        #endregion
    }
}