namespace Borentra.Models
{
    using System.Collections.Generic;

    public class Master<T>
    {
        #region Properties
        public T Display
        {
            get;
            set;
        }

        public IEnumerable<T> Manifest
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