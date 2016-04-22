namespace Borentra.Controllers
{
    using Borentra.Core;
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    [Authorize]
    public class DashboardController : Controller
    {
        #region Members
        /// <summary>
        /// History Core
        /// </summary>
        private readonly HistoryCore historyCore = new HistoryCore();

        /// <summary>
        /// Badge Core
        /// </summary>
        private readonly BadgeCore badgeCore = new BadgeCore();

        /// <summary>
        /// Item Request Core
        /// </summary>
        private readonly ItemRequestCore itemRequestCore = new ItemRequestCore();

        /// <summary>
        /// Activity Core
        /// </summary>
        private readonly ActivityCore activityCore = new ActivityCore();

        /// <summary>
        /// Item Core
        /// </summary>
        private readonly ItemCore itemCore = new ItemCore();

        /// <summary>
        /// Borrow Core
        /// </summary>
        private readonly BorrowCore borrowCore = new BorrowCore();

        /// <summary>
        /// Free Core
        /// </summary>
        private readonly FreeCore freeCore = new FreeCore();

        /// <summary>
        /// Profile Core
        /// </summary>
        private readonly ProfileCore profileCore = new ProfileCore();

        /// <summary>
        /// Melissa Core
        /// </summary>
        private readonly MelissaCore melissaCore = new MelissaCore();

        /// <summary>
        /// Statistics Core
        /// </summary>
        private readonly StatisticsCore statsCore = new StatisticsCore();

        /// <summary>
        /// Trade Core
        /// </summary>
        private readonly TradeCore tradeCore = new TradeCore();

        /// <summary>
        /// Rent Core
        /// </summary>
        private readonly RentCore rentCore = new RentCore();
        #endregion

        #region Methods
        private void GetCookie(Guid userId)
        {
            try
            {
                var themeCookie = Request.Cookies[HomeController.ThemeCookieName];
                if (null != themeCookie)
                {
                    if (profileCore.SaveTheme(userId, themeCookie.Value))
                    {
                        themeCookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Set(themeCookie);
                    }
                }
            }
            catch
            {
            }
        }
        // GET:/Dashboard
        public ActionResult Index()
        {
            var userId = User.Identifier();

            this.GetCookie(userId);

            var dashboard = profileCore.Load<MyProfile>(userId);
            var profile = profileCore.SearchSingle<MyProfile>(userId, null, userId);

            profile.Badges = badgeCore.Search(userId);

            if (null != profile)
            {
                profile.Items = itemCore.Search(userId, null, null, null, userId);

                var shares = this.borrowCore.Lent(userId);

                profile.PendingRequests = from br in shares
                                          where br.Status == BorrowStatus.Pending
                                          select br;

                profile.Lent = from br in shares
                               where br.Status == BorrowStatus.Accepted
                               select br;

                shares = this.borrowCore.Borrowed(userId);

                profile.BorrowRequests = from br in shares
                                         where br.Status == BorrowStatus.Pending
                                         select br;

                profile.Borrowed = from br in shares
                                   where br.Status == BorrowStatus.Accepted
                                   select br;

                profile.PendingTradeRequests = this.tradeCore.SearchItemTrade(userId);
                profile.TradeRequests = this.tradeCore.SearchItemTrade(null, userId);

                this.LoadItems(profile.PendingRequests, true);
                this.LoadItems(profile.BorrowRequests);
                this.LoadItems(profile.Lent, true);
                this.LoadItems(profile.Borrowed);

                profile.FreeAsk = this.LoadItems(freeCore.Search(userId), true) as IEnumerable<ItemFree>;
                profile.FreeRequested = this.LoadItems(freeCore.Search(null, userId)) as IEnumerable<ItemFree>;

                var fulfillments = from f in this.itemRequestCore.SearchFulfill(userId)
                                   where f.Status == RequestStatus.Pending
                                   select f;

                profile.FulfillMine = from f in fulfillments
                                      where f.IsMine
                                      select f;

                profile.FulfillOthers = from f in fulfillments
                                        where !f.IsMine
                                        select f;

                var rent = from r in this.rentCore.Rented(userId)
                           where r.Status != RentalStatus.Rejected
                            && r.Status != RentalStatus.Returned
                            select r;
                rent = this.LoadItems(rent, true) as IEnumerable<ItemRental>;

                profile.RentAsks = from r in rent
                                          where r.Status == RentalStatus.Pending
                                          select r;

                profile.Renting = from r in rent
                               where r.Status == RentalStatus.Accepted
                               select r;

                rent = from r in this.rentCore.Requested(userId)
                           where r.Status != RentalStatus.Rejected
                            && r.Status != RentalStatus.Returned
                            select r;
                rent = this.LoadItems(rent) as IEnumerable<ItemRental>;

                profile.RentRequests = from r in rent
                                   where r.Status == RentalStatus.Pending
                                   select r;

                profile.Rented = from r in rent
                                 where r.Status == RentalStatus.Accepted
                                 select r;

                dashboard.Info = profile;
            }

            return View(dashboard);
        }

        // GET: /Dashboard/Welcome
        public ActionResult Welcome()
        {
            var userId = User.Identifier();

            this.GetCookie(userId);

            var dashboard = profileCore.Load<MyProfile>(userId);
            dashboard.Info = this.profileCore.SearchSingle<MyProfile>(userId, null, userId);
            return this.View(dashboard);
        }

        // GET: /Dashboard/Conversation
        public ActionResult Conversation()
        {
            var userId = User.Identifier();
            var dashboard = this.profileCore.Load<Guid>(userId);
            dashboard.Info = userId;

            return View(dashboard);
        }

        // GET: /Dashboard/History
        public ActionResult History()
        {
            var userId = User.Identifier();
            var dashboard = this.profileCore.Load<History>(userId);
            dashboard.Info = this.historyCore.MyHistory(userId);

            return View(dashboard);
        }
        
        // GET: /Dashboard/Interests
        public ActionResult Interests(string interest)
        {
            var userId = User.Identifier();
            var page = profileCore.Load<MyProfile>(userId);
            var profile = profileCore.SearchSingle<MyProfile>(userId, null, userId);
            page.Info = profile;
            ViewBag.Theme = interest;
            return View(page);
        }

        // GET: /Dashboard/Settings
        public ActionResult Settings()
        {
            var userId = User.Identifier();
            var dashboard = this.profileCore.Load<Profile>(userId);
            dashboard.Info = profileCore.SearchSingle(userId, null, userId);

            return View(dashboard);
        }

        // GET: /Dashboard/Friends
        public ActionResult Friends()
        {
            var userId = User.Identifier();
            var dashboard = this.profileCore.Load<ProfileMaster>(userId);

            var master = new ProfileMaster()
            {
                Display = profileCore.SearchSingle(userId, null, userId),
            };

            master.Friends = profileCore.Friends(master.Display, short.MaxValue);

            master.Manifest = (from p in master.Friends
                               where p.Longitude != 0 && p.Latitude != 0
                               select p).ToList();

            dashboard.Info = master;

            return View(dashboard);
        }


        // GET: /Dashboard/FriendOffers
        public ActionResult FriendOffers()
        {
            var userId = User.Identifier();
            var dashboard = this.profileCore.Load<IEnumerable<Item>>(userId);

            dashboard.Info = this.itemCore.Search(null, OfferType.Unknown, null, short.MaxValue, userId, true);

            return View(dashboard);
        }

        // GET: /Dashboard/Wanted
        public ActionResult Wanted()
        {
            var userId = User.Identifier();
            var dashboard = this.profileCore.Load<IEnumerable<ItemRequest>>(userId);
            dashboard.Info = itemRequestCore.Search(userId, userId);
            return View(dashboard);
        }

        // GET: /Dashboard/Offers
        public ActionResult Offers()
        {
            var userId = User.Identifier();
            var dashboard = this.profileCore.Load<MyProfile>(userId);

            dashboard.Info = new MyProfile()
            {
                Items = itemCore.Search(userId, null, null, short.MaxValue, userId),
            };

            return View(dashboard);
        }

        // GET: /Dashboard/Trades
        public ActionResult Trades()
        {
            var userId = User.Identifier();
            var dashboard = this.profileCore.Load<MyProfile>(userId);

            dashboard.Info = profileCore.SearchSingle<MyProfile>(userId, null, userId);
            dashboard.Info.PendingTradeRequests = this.tradeCore.SearchItemTrade(userId);
            dashboard.Info.TradeRequests = this.tradeCore.SearchItemTrade(null, userId);

            return View(dashboard);
        }

        [Authorize(Roles = "staff")]
        public ActionResult EditOffer(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return this.RedirectToAction("offers");
            }

            var userId = User.Identifier();
            var dashboard = this.profileCore.Load<Item>(userId);

            dashboard.Info = this.itemCore.GetItem(null, key, userId);

            if (null != dashboard.Info && dashboard.Info.IsMine)
            {
                return this.View(dashboard);
            }
            else
            {
                return this.RedirectToAction("offers");
            }
        }

        [Authorize(Roles="staff")]
        public ActionResult EditRequest(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return this.RedirectToAction("wanted");
            }

            var userId = User.Identifier();
            var dashboard = this.profileCore.Load<ItemRequest>(userId);

            dashboard.Info = this.itemRequestCore.Get(key, null, userId);

            if (null != dashboard.Info && dashboard.Info.IsMine)
            {
                return this.View(dashboard);
            }
            else
            {
                return this.RedirectToAction("wanted");
            }
        }

        /// <summary>
        /// Load Items
        /// </summary>
        private IEnumerable<ItemAction> LoadItems(IEnumerable<ItemAction> items, bool isMine = false)
        {
            foreach (var item in items)
            {
                item.Item = new Item()
                {
                    Identifier = item.ItemIdentifier,
                    IsMine = isMine,
                };
            }

            return items;
        }
        #endregion
    }
}