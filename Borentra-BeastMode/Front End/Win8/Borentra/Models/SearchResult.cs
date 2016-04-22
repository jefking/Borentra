namespace Borentra.Models
{
    using System;
    using System.Collections.Generic;

    public class SearchResult
    {
        #region Properties
        public string Title
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }
        public string Key
        {
            get;
            set;
        }
        public Reference Type
        {
            get;
            set;
        }
        public string Thumbnail
        {
            get;
            set;
        }
        #endregion
    }
}