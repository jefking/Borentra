namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using Borentra.Models.DataTransferObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TradeCore
    {
        #region Methods
        public IEnumerable<Trade> SearchItemTrade(Guid? receiver = null, Guid? requester = null)
        {
            var sproc = new GoodsSearchItemTrade()
            {
                RequesterIdentifier = requester,
                ReceiverIdentifier = receiver,
            };

            var data = sproc.CallObjects<ItemTradeDTO>();
            return from tr in data
                   group tr by tr.TradeIdentifier into g
                   select new Trade(g);
        }

        public IEnumerable<Trade> SearchTrade(Guid? receiver = null, Guid? requester = null)
        {
            var sproc = new GoodsSearchTrade()
            {
                RequesterIdentifier = requester,
                ReceiverIdentifier = receiver,
            };

            var data = sproc.CallObjects<ItemTradeDTO>();
            return from tr in data
                   group tr by tr.TradeIdentifier into g
                   select new Trade(g);
        }
        #endregion
    }
}