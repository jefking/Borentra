namespace Borentra.Models
{
    using System;
    using System.Collections.Generic;

    public class TradeRequest
    {
        #region Properties
        public List<Guid> ItemIdentifiers
        {
            get;
            set;
        }
        #endregion
    }
}