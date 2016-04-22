namespace Borentra
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    /// <summary>
    /// Web API Application
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        #region Methods
        /// <summary>
        /// Application Start
        /// </summary>
        protected void Application_Start()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }

        /// <summary>
        /// Application Begin Request
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var host = Request.Url.DnsSafeHost.ToLowerInvariant();
            var deprecatedDomains = new string[] { "ccswaps.com", "www.ccswaps.com"
                                                    , "ccborrow.com", "www.ccborrow.com"
                                                    , "ccrenter.com", "www.ccrenter.com"
                                                    , "w.borentra.com", "ww.borentra.com"};

            foreach (var domain in deprecatedDomains)
            {
                if (host == domain)
                {
                    Response.RedirectPermanent(string.Format("http://www.borentra.com?redirect={0}", domain));
                    break;
                }
            }
        } 
        #endregion
    }
}