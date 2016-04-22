namespace Borentra.Controllers
{
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    /// Profile Controller
    /// </summary>
    public class ProfileController : Controller
    {
        #region Members
        /// <summary>
        /// Badge Core
        /// </summary>
        private readonly BadgeCore badgeCore = new BadgeCore();

        /// <summary>
        /// Item Request Core
        /// </summary>
        private readonly ItemRequestCore itemRequestCore = new ItemRequestCore();

        /// <summary>
        /// Profile Core
        /// </summary>
        private readonly ProfileCore profileCore = new ProfileCore();

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
        #endregion

        #region Methods
        /// <summary>
        /// Profile Directory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Unique(Guid? id, string key = null)
        {
            if (!id.HasValue && string.IsNullOrWhiteSpace(key))
            {
                return RedirectToAction("Index");
            }

            var callerId = User.IdentifierSafe();

            var profile = new ProfileMaster()
            {
                Display = profileCore.SearchSingle(id, key, callerId),
            };

            if (null != profile.Display)
            {
                profile.Display.Badges = badgeCore.Search(profile.Display.Identifier);

                profile.Display.Activities = activityCore.UserSearch(profile.Display.Identifier);

                profile.Display.Items = itemCore.Search(profile.Display.Identifier, null, null, short.MaxValue, callerId);

                profile.ItemRequests = itemRequestCore.Search(profile.Display.Identifier, callerId);

                profile.Lent = from s in this.borrowCore.Lent(profile.Display.Identifier)
                               where s.Status == BorrowStatus.Returned
                               select s;
                profile.Borrowed = from s in this.borrowCore.Borrowed(profile.Display.Identifier)
                               where s.Status == BorrowStatus.Returned
                               select s;

                return View(profile);
            }
            else if (!string.IsNullOrWhiteSpace(key))
            {
                return Redirect(ProfileCore.SearchUrl(key, "unknown_key"));
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        /// <summary>
        /// /Profile/Search
        /// </summary>
        /// <param name="s">Search</param>
        /// <returns></returns>
        public ActionResult Index(string s, Guid? user)
        {
            var callerId = User.IdentifierSafe();

            if (user.HasValue && user.Value == callerId.Value)
            {
                return RedirectToAction("friends", "dashboard");
            }

            var results = new SearchResults<Profile>()
            {
                SearchDisplayText = s,
                Manifest = profileCore.Search(s, user, callerId),
            };
            
            return View(results);
        }

        [Authorize]
        public ActionResult Trade(Guid? id = null, string key = null)
        {
            var callerId = User.Identifier();

            var trader = profileCore.SearchSingle(callerId, null, callerId);
            var with = profileCore.SearchSingle(id, key, callerId);
            var selection = new TradeSelection()
            {
                TraderDisplayName = trader.Name,
                TraderFacebookId = trader.FacebookId,
                TraderIdentifier = callerId,

                WithDisplayName = with.Name,
                WithIdentifier = with.Identifier,
                WithFacebookId = with.FacebookId,
                WithStatus = with.Status,
            };
            
            return View("Trade", selection);
        }
        #endregion
    }
}