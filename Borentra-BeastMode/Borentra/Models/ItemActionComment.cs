namespace Borentra.Models
{
    using Borentra.Core;
    using System;

    /// <summary>
    /// Item Share Comment
    /// </summary>
    public class ItemActionComment : IIdentifier, IUserIdentifier, IComment
    {
        #region Properties
        public Guid Identifier
        {
            get;
            set;
        }

        public Guid ItemShareIdentifier
        {
            get;
            set;
        }

        public Guid UserIdentifier
        {
            get;
            set;
        }

        public string Comment
        {
            get;
            set;
        }

        public DateTime CreatedOn
        {
            get;
            set;
        }

        public long UserFacebookId
        {
            get;
            set;
        }

        public string UserName
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
        #endregion
    }
}