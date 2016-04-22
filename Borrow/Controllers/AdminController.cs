namespace Borentra.Controllers
{
    using Borentra.Core;
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Admin Controller
    /// </summary>
    [Authorize(Roles = "staff")]
    public class AdminController : Controller
    {
        #region Members
        /// <summary>
        /// Company Core
        /// </summary>
        private readonly CompanyCore companyCore = new CompanyCore();

        /// <summary>
        /// Profile Core
        /// </summary>
        private readonly ProfileCore profileCore = new ProfileCore();

        /// <summary>
        /// Email Core
        /// </summary>
        private readonly EmailCore emailCore = new EmailCore();

        /// <summary>
        /// Statistics Core
        /// </summary>
        private readonly StatisticsCore statsCore = new StatisticsCore();

        /// <summary>
        /// Borrow Core
        /// </summary>
        private readonly BorrowCore borrowCore = new BorrowCore();

        /// <summary>
        /// Item Core
        /// </summary>
        private readonly ItemCore itemCore = new ItemCore();

        /// <summary>
        /// Item Request Core
        /// </summary>
        private readonly ItemRequestCore itemRequestCore = new ItemRequestCore();

        /// <summary>
        /// Admin Core
        /// </summary>
        private readonly AdminCore adminCore = new AdminCore();

        /// <summary>
        /// Social Core
        /// </summary>
        private readonly SocialCore socialCore = new SocialCore();
        #endregion

        #region Methods
        // GET: /Admin/
        public ActionResult Index()
        {
            ViewBag.Totals = this.statsCore.Totals();

            return View();
        }

        // GET: /Admin/NewsLetter
        public ActionResult NewsLetter()
        {
            return this.View();
        }
        public ActionResult MarketTest()
        {
            var data = this.adminCore.TestSignUps();
            return this.View(data);
        }
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SendNewsLetter(string title = null, string all = null, HttpPostedFileBase file = null)
        {
            var userId = User.Identifier();
            var body = string.Empty;
            using (var reader = new StreamReader(file.InputStream))
            {
                body = reader.ReadToEnd();
            }

            if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(body))
            {
                var toAll = all == "on";
                var emailCore = new EmailCore();
                IEnumerable<Profile> profiles = null;
                if (toAll)
                {
                    profiles = this.profileCore.Search<Profile>(null, null, null, true, short.MaxValue);
                }
                else
                {
                    profiles = new List<Profile>(1);
                    var profile = new Profile() { Identifier = userId };
                    ((IList<Profile>)profiles).Add(profile);
                }

                if (null != profiles)
                {
                    var loaded = new List<Profile>();
                    foreach (var profile in profiles)
                    {
                        loaded.Add(this.profileCore.SearchSingle<Profile>(profile.Identifier, null, userId, true));
                    }

                    emailCore.NewsLetter(loaded, title, body);
                }
            }

            return this.RedirectToAction("Newsletter");
        }

        // GET: /Admin/Items
        public ActionResult Items(short count = 10)
        {
            var items = this.adminCore.Items(count);

            return this.View(items);
        }

        // GET: /Admin/ItemRequests
        public ActionResult ItemRequests(short count = 10)
        {
            var requests = this.adminCore.ItemRequests(count);

            return this.View(requests);
        }

        //
        // GET: /Admin/Users
        public ActionResult Users(Guid? id, short count = 10)
        {
            if (id.HasValue && Guid.Empty != id.Value)
            {
                var profile = profileCore.SearchSingle<ProfileFull>(id.Value, null, null, true);
                if (null != profile)
                {
                    if (!string.IsNullOrWhiteSpace(profile.FacebookAccessToken))
                    {
                        var fbCore = new FacebookCore();
                        if (string.IsNullOrWhiteSpace(profile.Location))
                        {
                            profile.Location = fbCore.Location(profile.FacebookAccessToken);

                            if (!string.IsNullOrWhiteSpace(profile.Location))
                            {
                                profileCore.Save(profile);
                            }
                        }

                        var emails = fbCore.ImportFriends(profile);
                        foreach (var email in emails)
                        {
                            emailCore.FriendSignedUp(email);
                        }
                    }
                }
            }

            return View(this.adminCore.Profiles(count));
        }


        // GET:/Admin/GoingGlobal
        public ActionResult GoingGlobal()
        {
            var profiles = from p in profileCore.Search(null, null, null, true, short.MaxValue)
                           where p.Longitude != 0 && p.Latitude != 0
                           select p;

            return View(profiles);
        }

        // GET: /Admin/CommunityShares
        public ActionResult CommunityShares()
        {
            return View(this.statsCore.CommunityShares());
        }

        // GET: /Admin/CommunityRents
        public ActionResult CommunityRents()
        {
            return View(this.statsCore.CommunityRents());
        }

        // GET: /Admin/CommunityTrades
        public ActionResult CommunityTrades()
        {
            return View(this.statsCore.CommunityTrades());
        }

        // GET: /Admin/CommunityGives
        public ActionResult CommunityGives()
        {
            return View(this.statsCore.CommunityFree());
        }

        // GET: /Admin/UserGrowth
        public ActionResult UserGrowth(byte? days = 7)
        {
            return View(this.statsCore.UserGrowth(days));
        }

        // GET: /Admin/LandingPage
        public ActionResult LandingPage(byte? days = 7)
        {
            return View(this.statsCore.LandingPage(days));
        }

        // GET: /Admin/ItemGrowth
        public ActionResult ItemGrowth(byte? days = 7)
        {
            return View(this.statsCore.ItemGrowth(days));
        }

        // GET: /Admin/DeviceGrowth
        public ActionResult DeviceGrowth(byte? days = 7)
        {
            return View(this.statsCore.DeviceGrowth(days));
        }

        public ActionResult Companies()
        {
            var companies = this.companyCore.Search();
            return this.View(companies);
        }

        public ActionResult CreateCompany()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult CreateCompany(string name, string description, string website, string email, string phone)
        {
            var company = new Company()
            {
                Description = description,
                Email = email,
                Name = name,
                PhoneNumber = phone,
                WebsiteUrl = website,
            };

            this.companyCore.Save(company);
            return this.RedirectToAction("Companies");
        }

        public ActionResult Tag(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return this.Redirect("index");
            }

            var item = this.itemCore.GetItem(null, key, null, true);

            if (null == item)
            {
                return this.Redirect("index");
            }
            else
            {
                item.SetCategories();
                return this.View(item);
            }
        }

        [HttpPost]
        public ActionResult SaveTag(Guid itemIdentifier, string tags)
        {
            if (Guid.Empty == itemIdentifier)
            {
                throw new ArgumentException("item idenfitier");
            }

            if (string.IsNullOrWhiteSpace(tags))
            {
                throw new ArgumentException();
            }

            var userId = User.Identifier();
            var save = new SocialTags()
            {
                ReferenceIdentifier = itemIdentifier,
                Tags = tags,
                UserIdentifier = userId,
            };

            this.socialCore.SaveTags(save);

            var item = this.itemCore.GetItem(itemIdentifier, null, null, true);

            return this.Redirect(ItemCore.BaseUrl(item.Key));
        }

        public ActionResult OffersUnTagged()
        {
            var items = this.adminCore.Items(short.MaxValue);

            var result = new List<Item>();
            foreach (var item in items)
            {
                item.SetCategories();
                if (null == item.Categories || 0 == item.Categories.Count())
                {
                    result.Add(item);
                }
            }

            return this.View(result);
        }
        #endregion
    }
}