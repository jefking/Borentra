namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.DataStore;
    using Borentra.Models;

    /// <summary>
    /// Data Core
    /// </summary>
    public class DataCore
    {
        #region Methods
        /// <summary>
        /// Cache Country Percentages
        /// </summary>
        public void CacheCountryPercentages()
        {
            var data = new StatsCountryPercentages().CallObjects<CountryPercentage>();
            var blob = new JsonContainer();
            blob.Save("globalheatmap", data);
        }

        /// <summary>
        /// Cache Home Page Data
        /// </summary>
        public void CacheHomePageData()
        {
            var data = new HomePageData()
            {
                Share = new GoodsRecentItemShare().CallObject<HomePageShare>(),
            };

            var blob = new JsonContainer();
            blob.Save("landingpage", data);
        }
        #endregion
    }
}