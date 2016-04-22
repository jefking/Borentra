namespace Borentra.Controllers
{
    using Borentra.DataStore;
    using Borentra.Models;
    using System;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Home Controller
    /// </summary>
    public class HomeController : Controller
    {
        #region Members
        /// <summary>
        /// Return URL Cookie Name
        /// </summary>
        private const string returnCookieName = "BorentraReturnUrl";

        /// <summary>
        /// Theme Cookie Name
        /// </summary>
        public const string ThemeCookieName = "BorentraTheme";

        /// <summary>
        /// Home Page
        /// </summary>
        private static HomePage homePage = null;
        #endregion

        #region Methods
        /// <summary>
        /// Home Page
        /// </summary>
        /// <returns>Action Result</returns>
        public ActionResult Index(string ReturnUrl = null)
        {
            if (base.User.Identity.IsAuthenticated)
            {
                var cookie = Request.Cookies[returnCookieName];
                if (null == cookie || string.IsNullOrWhiteSpace(cookie.Value))
                {
                    return this.RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Set(cookie);

                    return Redirect(cookie.Value);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(ReturnUrl))
                {
                    var cookie = new HttpCookie(returnCookieName, ReturnUrl)
                    {
                        Expires = DateTime.Now.AddHours(1),
                    };

                    Response.Cookies.Add(cookie);
                }
                var theme = Request.QueryString["theme"];
                if (!string.IsNullOrWhiteSpace(theme))
                {
                    var cookie = new HttpCookie(ThemeCookieName, theme)
                    {
                        Expires = DateTime.Now.AddDays(31),
                    };

                    Response.Cookies.Add(cookie);
                }

                var emptyHomePage = new HomePage()
                {
                    Share = new ItemShare(),
                };

                try
                {
                    if (null == homePage)
                    {
                        var blob = new JsonContainer();
                        homePage = blob.Get<HomePage>("landingpage");
                    }
                }
                catch
                {
                }

                return View(homePage ?? emptyHomePage);
            }
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return this.RedirectToAction("Index", "Dashboard");
        }

        // GET: /Home/About
        public ActionResult About()
        {
            return View();
        }

        // GET: /Home/AboutUs
        public ActionResult AboutUs()
        {
            return this.RedirectToActionPermanent("About");
        }

        // GET: /Home/Contact
        public ActionResult Contact()
        {
            return View();
        }

        // GET: /Home/ContactUs
        public ActionResult ContactUs()
        {
            return this.RedirectToActionPermanent("Contact");
        }

        // GET: /Home/Mantra
        public ActionResult Mantra()
        {
            return View();
        }

        public ActionResult Mobile()
        {
            return View();
        }

        public ActionResult Business()
        {
            return View("../Company/Index");
        }

        public ActionResult Terms()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }
        #endregion
    }
}