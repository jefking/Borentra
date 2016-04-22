namespace Borentra.Models
{
    using Borentra.Core;
    using System;

    /// <summary>
    /// Social Comment
    /// </summary>
    public class SocialComment : Social, IComment
    {
        #region Properties
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

        public long OwnerFacebookId
        {
            get;
            set;
        }

        public string OwnerName
        {
            get;
            set;
        }

        public string OwnerKey
        {
            get;
            set;
        }

        public Uri OwnerPicture
        {
            get
            {
                return FacebookCore.Picture(this.OwnerFacebookId);
            }
        }

        public bool IsMine
        {
            get;
            set;
        }

        public Guid Identifier
        {
            get;
            set;
        }
        #endregion
    }
}