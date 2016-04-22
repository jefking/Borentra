namespace Borentra.Models.DataTransferObjects
{
    using System;

    /// <summary>
    /// Date is loaded into this object before it is 
    /// converted into a view model
    /// </summary>
    public class ItemTradeDTO
    {
        #region Trade Properties

        public Guid TradeIdentifier
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

        public Guid RequesterIdentifier
        {
            get;
            set;
        }

        public string RequesterName
        {
            get;
            set;
        }

        public string RequesterEmail
        {
            get;
            set;
        }

        public string ReceiverKey
        {
            get;
            set;
        }

        public long ReceiverFacebookId
        {
            get;
            set;
        }

        public Guid ReceiverIdentifier
        {
            get;
            set;
        }

        public string ReceiverName
        {
            get;
            set;
        }

        public string ReceiverEmail
        {
            get;
            set;
        }

        public DateTime CreatedOn
        {
            get;
            set;
        }
        
        public DateTime AcceptedOn
        {
            get;
            set;
        }
        public DateTime DeletedOn
        {
            get;
            set;
        }
        public DateTime ModifiedOn
        {
            get;
            set;
        }
        public DateTime RejectedOn
        {
            get;
            set;
        }
        #endregion

        #region Item Properties
        public Guid ItemIdentifier
        {
            get;
            set;
        }

        public Guid ItemOwnerIdentifier
        {  
            get;
            set;
        }

        public string ItemTitle
        {
            get;
            set;
        }

        public string TradeItemKey
        {
            get;
            set;
        }

        public string ItemDescription
        {
            get;
            set;
        }

        public string PrimaryImagePathFormat
        {
            get;
            set;
        }
        #endregion

        internal Item toItem()
        {
            return new Item()
            {
                Identifier = this.ItemIdentifier,
                UserIdentifier = this.ItemOwnerIdentifier,
                Title = this.ItemTitle,
                Key = this.TradeItemKey,
                Description = this.ItemDescription,
                PrimaryImagePathFormat = this.PrimaryImagePathFormat
            };
        }
    }
}