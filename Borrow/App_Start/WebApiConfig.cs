namespace Borentra
{
    using System.Web.Http;

    /// <summary>
    /// Web Api Config
    /// </summary>
    public static class WebApiConfig
    {
        #region Methods
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="config">Http Configuration</param>
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { }
            );

            config.Routes.MapHttpRoute(
                name: "MobileApi",
                routeTemplate: "api/mobile/{controller}/{action}",
                defaults: new { }
            );
        }
        #endregion
    }
}