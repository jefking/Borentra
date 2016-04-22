namespace Borentra.DataAccessLayer.Table
{
    using Microsoft.WindowsAzure.Storage.Table;

    /// <summary>
    /// Bing Query Entry
    /// </summary>
    public class BingQueryEntry : TableEntity
    {
        #region Properties
        /// <summary>
        /// Url
        /// </summary>
        public string Url
        {
            get;
            set;
        }
        public string ThumbnailUrl
        {
            get;
            set;
        }
        #endregion
    }
}