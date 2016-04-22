namespace Borentra.API
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Web.Http;
    using System.Web.Mvc;

    public class WebApiApplication : System.Web.HttpApplication
    {
        #region Methods
        protected void Application_Start()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
        #endregion
    }
}