namespace Borentra.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Profile History
    /// </summary>
    public class History
    {
        #region Properties
        public IEnumerable<OfferHistory> All
        {
            get;
            set;
        }
        #endregion
    }
}