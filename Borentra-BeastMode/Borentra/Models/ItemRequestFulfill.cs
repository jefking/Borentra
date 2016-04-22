namespace Borentra.Models
{
    using Borentra.Core;
    using System;

    public class ItemRequestFulfill : IIdentifier, IUserIdentifier, IComment
    {
        #region Properties
        public Guid Identifier
        {
            get;
            set;
        }

        public Guid ItemRequestIdentifier
        {
            get;
            set;
        }

        public Guid UserIdentifier
        {
            get;
            set;
        }

        public RequestStatus Status
        {
            get;
            set;
        }

        public bool IsMine
        {
            get;
            set;
        }

        public string Comment
        {
            get;
            set;
        }

        public bool WillRent
        {
            get;
            set;
        }

        public bool WillShare
        {
            get;
            set;
        }

        public bool WillTrade
        {
            get;
            set;
        }

        public bool WillGive
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
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

        public Guid RequesterIdentifier
        {
            get;
            set;
        }
        #endregion
    }
}