namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class HistoryCore
    {
        #region Properties
        public IEnumerable<OfferHistory> My(Guid userId)
        {
            if (Guid.Empty == userId)
            {
                throw new ArgumentException("userId");
            }

            var sproc = new GoodsMyHistory()
            {
                UserIdentifier = userId
            };

            return sproc.CallObjects<OfferHistory>();
        }
        public History MyHistory(Guid userId)
        {
            if (Guid.Empty == userId)
            {
                throw new ArgumentException("userId");
            }

            return new History()
            {
                All = this.My(userId).OrderByDescending(h => h.On),
            };
        }
        #endregion
    }
}