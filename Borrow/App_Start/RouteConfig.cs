namespace Borentra
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Route Config
    /// </summary>
    public class RouteConfig
    {
        #region Methods
        /// <summary>
        /// Register Routes
        /// </summary>
        /// <param name="routes">Routes</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;

            routes.MapRoute(name: "Profile Search (deprecated)", url: "Profile/Search", defaults: new { controller = "Profile", action = "Index" });
            routes.MapRoute(name: "Profile Search", url: "profile", defaults: new { controller = "Profile", action = "Index" });
            routes.MapRoute(name: "Profile SEO", url: "profile/{key}", defaults: new { controller = "Profile", action = "Unique" });
            routes.MapRoute(name: "Profile Trade", url: "profile/{key}/trade", defaults: new { controller = "Profile", action = "Trade" });
            routes.MapRoute(name: "Profile Requests", url: "profile/{key}/requests", defaults: new { controller = "Profile", action = "ProfileRequests" });

            routes.MapRoute(name: "Product Search", url: "Item", defaults: new { controller = "Item", action = "Index" });
            routes.MapRoute(name: "Product SEO", url: "Item/{key}", defaults: new { controller = "Item", action = "Unique" });

            routes.MapRoute(name: "Offer Search", url: "Offer", defaults: new { controller = "Item", action = "Index" });
            routes.MapRoute(name: "Offer SEO", url: "Offer/{key}", defaults: new { controller = "Item", action = "Unique" });

            routes.MapRoute(name: "Request Search", url: "Requests", defaults: new { controller = "Requests", action = "Index" });
            routes.MapRoute(name: "Request SEO", url: "Requests/{key}", defaults: new { controller = "Requests", action = "Unique" });

            routes.MapRoute(name: "Wanted Search", url: "Wanted", defaults: new { controller = "Requests", action = "Index" });
            routes.MapRoute(name: "Wanted SEO", url: "Wanted/{key}", defaults: new { controller = "Requests", action = "Unique" });

            routes.MapRoute(name: "Company Queue", url: "Company", defaults: new { controller = "Company", action = "Index" });
            routes.MapRoute(name: "Company SEO", url: "Company/{key}", defaults: new { controller = "Company", action = "Unique" });

            routes.MapRoute(name: "Group Queue", url: "Group", defaults: new { controller = "Group", action = "Index" });
            routes.MapRoute(name: "Group SEO", url: "Group/{key}", defaults: new { controller = "Group", action = "Unique" });

            routes.MapRoute(name: "Free Search", url: "Free", defaults: new { controller = "Free", action = "Index" });

            routes.MapRoute(name: "Share Search", url: "Share", defaults: new { controller = "Share", action = "Index" });

            routes.MapRoute(name: "Rent Search", url: "Rent", defaults: new { controller = "Rent", action = "Index" });

            routes.MapRoute(name: "Trade Search", url: "Trade", defaults: new { controller = "Trade", action = "Index" });

            routes.MapRoute(name: "Activity Post", url: "Activity/{activityId}", defaults: new { controller = "Activity", action = "Index" });

            // Edit Offers & Requests
            routes.MapRoute(name: "Edit Offer", url: "Dashboard/Offer/{key}", defaults: new { controller = "Dashboard", action = "EditOffer" });
            routes.MapRoute(name: "Edit Request", url: "Dashboard/Wanted/{key}", defaults: new { controller = "Dashboard", action = "EditRequest" });

            // Error URLs (404, 500, Registration Failures)
            routes.MapRoute(name: "Generic Error", url: "error", defaults: new { controller = "Error", action = "Error404" });
            routes.MapRoute(name: "404 Not Found", url: "error/404", defaults: new { controller = "Error", action = "Error404" });
            routes.MapRoute(name: "500 Service Unavailable", url: "error/500", defaults: new { controller = "Error", action = "Error500" });
            routes.MapRoute(name: "Unable to Register on Facebook", url: "error/registration", defaults: new { controller = "Error", action = "ErrorRegistration" });

            routes.MapRoute(name: "Interest", url: "dashboard/interests/{interest}", defaults: new { controller = "Dashboard", action = "Interests" });

            routes.MapRoute(name: "About", url: "about", defaults: new { controller = "Home", action = "About" });
            routes.MapRoute(name: "Contact", url: "contact", defaults: new { controller = "Home", action = "Contact" });
            routes.MapRoute(name: "Mantra", url: "mantra", defaults: new { controller = "Home", action = "Mantra" });
            routes.MapRoute(name: "Terms Of Use", url: "terms", defaults: new { controller = "Home", action = "Terms" });
            routes.MapRoute(name: "Privacy Policy", url: "privacy", defaults: new { controller = "Home", action = "Privacy" });
            routes.MapRoute(name: "Mobile", url: "mobile", defaults: new { controller = "Home", action = "Mobile" });
            routes.MapRoute(name: "Business Services", url: "business", defaults: new { controller = "Home", action = "Business" });

            // Mother fucking defaults
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" });
        }
        #endregion
    }
}