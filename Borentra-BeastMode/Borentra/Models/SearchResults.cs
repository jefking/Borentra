namespace Borentra.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Search Results
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SearchResults<T>
    {
        #region Properties
        public IEnumerable<T> Manifest
        {
            get;
            set;
        }

        public Profile User
        {
            get;
            set;
        }

        public string SearchDisplayText
        {
            get;
            set;
        }
        #endregion
    }
}