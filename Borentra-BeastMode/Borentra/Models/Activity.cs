namespace Borentra.Models
{
    using Borentra.Core;
    using System;

    /// <summary>
    /// User Activity
    /// </summary>
    public class Activity : IIdentifier, IUserIdentifier, IReferenceType
    {
        #region Properties
        public Guid Identifier
        {
            get;
            set;
        }

        public Guid? ReferenceIdentifier
        {
            get;
            set;
        }

        public string ReferenceKey
        {
            get;
            set;
        }

        public Guid UserIdentifier
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public Reference Type
        {
            get;
            set;
        }

        public string UserKey
        {
            get;
            set;
        }

        public long UserFacebookId
        {
            get;
            set;
        }

        public Uri UserPicture
        {
            get
            {
                return FacebookCore.Picture(this.UserFacebookId);
            }
        }

        public string UserDisplayName
        {
            get;
            set;
        }

        public UserContext UserContext
        {
            get;
            set;
        }

        public DateTime ModifiedOn
        {
            get;
            set;
        }

        public int FavoriteCount
        {
            get;
            set;
        }

        public int CommentCount
        {
            get;
            set;
        }

        public bool CallerFavorited
        {
            get;
            set;
        }

        public string ImagePathFormat
        {
            get;
            set;
        }

        public string ImageThumbnail
        {
            get
            {
                return ImageCore.Thumbnail(this.ImagePathFormat);
            }
        }
        #endregion
    }
}