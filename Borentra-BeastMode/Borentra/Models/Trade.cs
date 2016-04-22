namespace Borentra.Models
{
    using Borentra.Models.DataTransferObjects;
    using System;
    using System.Collections.Generic;

    public class Trade
    {
        #region Properties
        public Guid TradeIdentifier
        {
            get;
            set;
        }

        public Profile Requester
        {
            get;
            set;
        }

        public Profile Receiver
        {
            get;
            set;
        }

        public List<Item> RequesterItems
        {
            get;
            set;
        }

        public List<Item> ReceiverItems
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

        #region Methods
        public Trade(IEnumerable<ItemTradeDTO> itemTrades)
        {
            if (itemTrades == null)
            {
                throw new ArgumentNullException("No items to buil trade object");
            }

            this.RequesterItems = new List<Item>();
            this.ReceiverItems = new List<Item>();

            var isFirstRun = true;
            foreach (var itemTrade in itemTrades)
            {
                if (isFirstRun)
                {
                    this.TradeIdentifier = itemTrade.TradeIdentifier;

                    this.Requester = new Profile()
                    {
                        Identifier = itemTrade.RequesterIdentifier,
                        FacebookId = itemTrade.RequesterFacebookId,
                        Key = itemTrade.RequesterKey,
                        Name = itemTrade.RequesterName,
                        Email = itemTrade.RequesterEmail
                    };

                    this.Receiver = new Profile()
                    {
                        Identifier = itemTrade.ReceiverIdentifier,
                        FacebookId = itemTrade.ReceiverFacebookId,
                        Key = itemTrade.ReceiverKey,
                        Name = itemTrade.ReceiverName,
                        Email = itemTrade.ReceiverEmail
                    };

                    this.CreatedOn = itemTrade.CreatedOn;
                    this.AcceptedOn = itemTrade.AcceptedOn;
                    this.DeletedOn = itemTrade.DeletedOn;
                    this.ModifiedOn = itemTrade.ModifiedOn;
                    this.RejectedOn = itemTrade.RejectedOn;

                    isFirstRun = false;
                }

                if (itemTrade.ItemOwnerIdentifier == Requester.Identifier)
                {
                    RequesterItems.Add(itemTrade.toItem());
                }
                else if (itemTrade.ItemOwnerIdentifier == Receiver.Identifier)
                {
                    ReceiverItems.Add(itemTrade.toItem());
                }
            }
        }
        #endregion
    }
}