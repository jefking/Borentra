namespace Borentra.Controllers.Api
{
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    // THIS IS A HACK CONTROLLER TO TEST MOBILE CONNEcZTIVITy
    public class MobileSocialController : ApiController
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
        #endregion

        #region Methods
        // GET:/API/Social/SearchActivity
        [HttpGet]
        public IEnumerable<Activity> SearchActivity(Guid userId)
        {
            return activityCore.Search(userId);
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

            favorite.UserIdentifier = favorite.UserIdentifier;
            var saved = this.socialCore.SaveFavorite(favorite);

            return null != saved ? this.socialCore.SearchFavorite(saved.ReferenceIdentifier) : null;
        }

        // GET:/API/Social/Favorite
        //[HttpGet]
        //public IEnumerable<SocialFavorite> Favorites(Guid referenceId)
        //{
        //    if (Guid.Empty == referenceId)
        //    {
        //        throw new ArgumentException("Reference Identifier");
        //    }

        //    var userId = User.Identifier();
        //    return this.socialCore.SearchFavorite(userId, referenceId);
        //}


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
            comment.UserIdentifier = comment.UserIdentifier;
            var newComment = this.socialCore.SaveComment(comment);

            return null != newComment ? this.socialCore.SearchComment(comment.UserIdentifier, newComment.ReferenceIdentifier) : null;
        }

        // GET:/API/Social/Comments
        [HttpGet]
        public IEnumerable<SocialComment> Comments(Guid userId, Guid referenceId)
        {
            if (Guid.Empty == referenceId)
            {
                throw new ArgumentException("Reference Identifier");
            }
            return this.socialCore.SearchComment(userId, referenceId);
        }
        #endregion
    }
}