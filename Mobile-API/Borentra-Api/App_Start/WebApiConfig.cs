namespace Borentra.API
{
    using Microsoft.Owin.Security.OAuth;
    using System.Web.Http;

    public static class WebApiConfig
    {
        #region Methods
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "{controller}"
            );
        }
        #endregion
    }
}