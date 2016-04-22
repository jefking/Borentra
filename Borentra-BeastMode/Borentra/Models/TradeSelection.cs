namespace Borentra.Models
{
    using Borentra.Core;
    using System;

    public class TradeSelection
    {
        #region Properties
        public string WithStatus
        {
            get;
            set;
        }
        public string WithDisplayName
        {
            get;
            set;
        }
        public long WithFacebookId
        {
            get;
            set;
        }
        public Uri WithPicture
        {
            get
            {
                return FacebookCore.Picture(this.WithFacebookId);
            }
        }
        public Guid WithIdentifier
        {
            get;
            set;
        }

        public string TraderDisplayName
        {
            get;
            set;
        }
        public long TraderFacebookId
        {
            get;
            set;
        }
        public Uri TraderPicture
        {
            get
            {
                return FacebookCore.Picture(this.TraderFacebookId);
            }
        }
        public Guid TraderIdentifier
        {
            get;
            set;
        }
        #endregion
    }
}