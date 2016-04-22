namespace Borentra
{
    using System.Web.Mvc;

    /// <summary>
    /// Filter Configuration
    /// </summary>
    public class FilterConfig
    {
        #region Methods
        /// <summary>
        /// Register Global Filters
        /// </summary>
        /// <param name="filters">Filters</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
        #endregion
    }
}