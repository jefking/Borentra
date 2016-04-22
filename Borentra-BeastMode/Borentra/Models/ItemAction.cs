namespace Borentra.Models
{
    using Borentra.Core;
    using System;

    /// <summary>
    /// Item Action
    /// </summary>
    public class ItemAction : IIdentifier
    {
        #region Properties
        public Guid Identifier
        {
            get;
            set;
        }

        public Guid ItemIdentifier
        {
            get;
            set;
        }

        public string ItemKey
        {
            get;
            set;
        }

        public string ItemTitle
        {
            get;
            set;
        }

        public string PrimaryImagePathFormat
        {
            get;
            set;
        }

        public string PrimaryImageThumbnail
        {
            get
            {
                return ImageCore.Thumbnail(PrimaryImagePathFormat);
            }
        }

        public Guid OwnerIdentifier
        {
            get;
            set;
        }

        public string OwnerDisplayName
        {
            get;
            set;
        }

        public string OwnerKey
        {
            get;
            set;
        }

        public string OwnerLocation
        {
            get;
            set;
        }

        public long OwnerFacebookId
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

        public string RequesterDisplayName
        {
            get;
            set;
        }

        public string RequesterKey
        {
            get;
            set;
        }

        public long RequesterFacebookId
        {
            get;
            set;
        }

        public Uri RequesterPicture
        {
            get
            {
                return FacebookCore.Picture(this.RequesterFacebookId);
            }
        }

        public string RequesterLocation
        {
            get;
            set;
        }

        public Guid RequesterUserIdentifier
        {
            get;
            set;
        }

        public Item Item
        {
            get;
            set;
        }
        #endregion
    }
}