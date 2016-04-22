namespace Borentra.Controllers.Api
{
    using Borentra.Core;
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using Borentra.Models.DataTransferObjects;
    using System;
    using System.Web.Http;

    [Authorize]
    public class TradeController : ApiController
    {
        #region Members
        /// <summary>
        /// Email Core
        /// </summary>
        private readonly EmailCore email = new EmailCore();

        /// <summary>
        /// Activity Core
        /// </summary>
        private readonly ActivityCore activity = new ActivityCore();
        #endregion

        #region Methods
        [HttpPost]
        [System.Web.Http.ActionName("Request")]
        public Trade RequestTrade(TradeRequest trade)
        {
            if (null == trade)
            {
                throw new ArgumentNullException("trade");
            }

            var list = string.Empty;
            foreach (var itemIdentifier in trade.ItemIdentifiers)
            {
                if (!string.IsNullOrEmpty(list))
                {
                    list += ",";
                }
                list += itemIdentifier.ToString();
            }

            var userId = User.Identifier();
            var sproc = new GoodsTradeRequest()
            {
                UserIdentifier = userId,
                ItemIdentifiers = list
            };

            var itemTrade = sproc.CallObjects<ItemTradeDTO>();
            var t = new Trade(itemTrade);
            if (null != t)
            {
                this.email.TradeRequest(t);
                this.activity.TradeRequested(itemTrade);
            }

            return t;
        }

        [HttpPost]
        [System.Web.Http.ActionName("Accept")]
        public Trade AcceptTrade(Guid tradeIdentifier)
        {
            if (Guid.Empty == tradeIdentifier)
            {
                throw new ArgumentException("tradeIdentifier");
            }

            var userId = User.Identifier();
            var sproc = new GoodsTradeAccept()
            {
                UserIdentifier = userId,
                TradeIdentifier = tradeIdentifier
            };

            var itemTrade = sproc.CallObjects<ItemTradeDTO>();
            var t = new Trade(itemTrade);

            if (null != t)
            {
                this.email.TradeAccepted(t);
                this.activity.TradeAccepted(itemTrade);
            }

            return t;
        }

        [HttpPost]
        [System.Web.Http.ActionName("Reject")]
        public Trade RejectTrade(Guid tradeIdentifier)
        {
            if (Guid.Empty == tradeIdentifier)
            {
                throw new ArgumentException("tradeIdentifier");
            }

            var userId = User.Identifier();
            var sproc = new GoodsTradeReject()
            {
                UserIdentifier = userId,
                TradeIdentifier = tradeIdentifier
            };

            var itemTrade = sproc.CallObjects<ItemTradeDTO>();
            var t = new Trade(itemTrade);

            if (null != itemTrade)
            {
                this.activity.TradeDeclined(itemTrade);
                this.email.TradeDeclined(t);
            }

            return new Trade(itemTrade);
        }

        [HttpPost]
        [System.Web.Http.ActionName("Delete")]
        public void DeleteTrade(Guid tradeIdentifier)
        {
            if (Guid.Empty == tradeIdentifier)
            {
                throw new ArgumentException("tradeIdentifier");
            }

            var userId = User.Identifier();
            var sproc = new GoodsTradeDelete()
            {
                UserIdentifier = userId,
                Identifier = tradeIdentifier
            };

            sproc.ExecuteNonQuery();
        }
        #endregion
    }
}