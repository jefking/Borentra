namespace Borentra.Models
{
    using System.Collections.Generic;

    public class ItemRequestMaster
    {
        #region Properties
        public ItemRequest Display
        {
            get;
            set;
        }

        public IEnumerable<Item> Manifest
        {
            get;
            set;
        }
        public IEnumerable<SearchResult> Results
        {
            get;
            set;
        }
        #endregion
    }
}