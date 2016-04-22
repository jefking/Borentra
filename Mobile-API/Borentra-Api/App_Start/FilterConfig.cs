namespace Borentra.API
{
    using System.Web.Mvc;

    public class FilterConfig
    {
        #region Methods
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
        #endregion
    }
}