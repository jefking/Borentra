namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.DataAccessLayer.Facebook;
    using Borentra.Models;
    using Borentra.Models.DataTransferObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Activity Core
    /// </summary>
    public class ActivityCore
    {
        #region Methods
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="userIdentifier">User Identifier</param>
        /// <returns>Activities</returns>
        public IEnumerable<Activity> Search(Guid userIdentifier, byte top = 20)
        {
            var proc = new UserSearchActivity()
            {
                UserIdentifier = userIdentifier,
                Maximum = top,
            };

            return proc.CallObjects<Activity>();
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <param name="userIdentifier">User Identifier</param>
        /// <returns>Activities</returns>
        public IEnumerable<Activity> UserSearch(Guid userIdentifier, byte top = 10)
        {
            var proc = new UserSearchUserActivity()
            {
                UserIdentifier = userIdentifier,
                Top = top,
            };

            return proc.CallObjects<Activity>();
        }

        #region Activities
        /// <summary>
        /// Status Update
        /// </summary>
        /// <param name="userIdentifier"></param>
        /// <param name="status"></param>
        public void StatusUpdate(Guid userIdentifier, string status)
        {
            var activity = new Activity()
            {
                Type = Reference.None,
                Text = string.Format("{0}", status),
                UserIdentifier = userIdentifier,
            };

            this.Save(activity);
        }

        /// <summary>
        /// Profile Update
        /// </summary>
        /// <param name="userIdentifier"></param>
        public void ProfileUpdate(Guid userIdentifier, MobileOS os = MobileOS.Unknown)
        {
            string osName = null;
            switch (os)
            {
                case MobileOS.Android:
                    osName = "Android";
                    break;
                case MobileOS.Windows8:
                    osName = "Windows 8.1";
                    break;
                case MobileOS.Blackberry:
                    osName = "Blackberry";
                    break;
                case MobileOS.iOS:
                    osName = "iPhone";
                    break;
                case MobileOS.WindowsPhone:
                    osName = "Windows Phone";
                    break;
            }
            
            var text = string.IsNullOrWhiteSpace(osName) ? "has updated their profile." : string.Format("has updated their profile from the {0} client.", osName);
            var activity = new Activity()
            {
                Type = Reference.None,
                Text = text,
                UserIdentifier = userIdentifier,
            };

            this.Save(activity);
        }

        /// <summary>
        /// Joined
        /// </summary>
        /// <param name="userIdentifier">User Identifer</param>
        /// <param name="location">Location</param>
        public void Joined(Guid userIdentifier, string location = null)
        {
            var text = "has joined all of us!";
            if (!string.IsNullOrWhiteSpace(location))
            {
                text = string.Format("has joined all of us! {0}.", location);
            }

            var activity = new Activity()
            {
                Type = Reference.None,
                Text = text,
                UserIdentifier = userIdentifier,
            };

            this.Save(activity);
        }


        /// <summary>
        /// New Friend
        /// </summary>
        /// <param name="email"></param>
        public void GotAFriend(FriendEmail email)
        {
            var activity = new Activity()
            {
                Type = Reference.User,
                Text = string.Format("became friends with {0}.", email.FriendDisplayName),
                UserIdentifier = email.UserIdentifier,
                ReferenceIdentifier = email.FriendIdentifier,
            };

            this.Save(activity);
        }

        #region Items
        public void NewItem(Item item)
        {
            var activity = new Activity()
            {
                Type = Reference.Item,
                Text = string.Format("added {0}", item.Title),
                UserIdentifier = item.UserIdentifier,
                ReferenceIdentifier = item.Identifier,
            };

            this.Save(activity);
        }

        public void UpdateItem(Item item)
        {
            var activity = new Activity()
            {
                Type = Reference.Item,
                Text = string.Format("updated {0}", item.Title),
                UserIdentifier = item.UserIdentifier,
                ReferenceIdentifier = item.Identifier,
            };

            this.Save(activity);
        }

        public void NewItemImage(ItemImageInput item)
        {
            var activity = new Activity()
            {
                Type = Reference.ItemImage,
                Text = string.Format("added a photo to an item"),
                UserIdentifier = item.UserIdentifier,
                ReferenceIdentifier = item.Identifier,
            };

            this.Save(activity);
        }

        public void NewItemRequestImage(ItemRequestImageInput item)
        {
            var activity = new Activity()
            {
                Type = Reference.ItemRequestImage,
                Text = string.Format("added a photo to a request"),
                UserIdentifier = item.UserIdentifier,
                ReferenceIdentifier = item.Identifier,
            };

            this.Save(activity);
        }
        #endregion

        #region Borrow
        public void RequestBorrow(ItemShare share)
        {
            var activity = new Activity()
            {
                Type = Reference.Item,
                Text = string.Format("has requested to borrow '{0}'.", share.ItemTitle),
                UserIdentifier = share.RequesterUserIdentifier,
                ReferenceIdentifier = share.ItemIdentifier,
            };

            this.Save(activity);
        }

        public void RejectBorrow(ItemShare share)
        {
            var activity = new Activity()
            {
                Type = Reference.Item,
                Text = string.Format("has found out that '{0}' is not available.", share.ItemTitle),
                UserIdentifier = share.RequesterUserIdentifier,
                ReferenceIdentifier = share.ItemIdentifier,
            };

            this.Save(activity);
        }

        public void AcceptBorrow(ItemShare share)
        {
            var activity = new Activity()
            {
                Type = Reference.Item,
                Text = string.Format("is now borrowing '{0}'", share.ItemTitle),
                UserIdentifier = share.RequesterUserIdentifier,
                ReferenceIdentifier = share.ItemIdentifier,
            };

            this.Save(activity);

            activity = new Activity()
            {
                Type = Reference.Item,
                Text = string.Format("is now lending '{0}'", share.ItemTitle),
                UserIdentifier = share.OwnerIdentifier,
                ReferenceIdentifier = share.ItemIdentifier,
            };

            this.Save(activity);
        }

        public void ReturnBorrow(ItemShare share)
        {
            var activity = new Activity()
            {
                Type = Reference.Item,
                Text = string.Format("has had '{0}' returned", share.ItemTitle),
                UserIdentifier = share.RequesterUserIdentifier,
                ReferenceIdentifier = share.ItemIdentifier,
            };

            this.Save(activity);

            activity = new Activity()
            {
                Type = Reference.User,
                Text = string.Format("has returned '{0}'", share.ItemTitle),
                UserIdentifier = share.OwnerIdentifier,
                ReferenceIdentifier = share.RequesterUserIdentifier,
            };

            this.Save(activity);
        }
        #endregion

        #region Free
        public void RequestFree(ItemFree item)
        {
            var activity = new Activity()
            {
                UserIdentifier = item.RequesterUserIdentifier,
                Type = Reference.Item,
                Text = string.Format("wants a free '{0}'.", item.ItemTitle),
                ReferenceIdentifier = item.ItemIdentifier,
            };

            this.Save(activity);
        }

        public void AcceptFree(ItemFree item)
        {
            var activity = new Activity()
            {
                UserIdentifier = item.OwnerIdentifier,
                Type = Reference.Item,
                Text = string.Format("gave away '{0}'.", item.ItemTitle),
                ReferenceIdentifier = item.ItemIdentifier,
            };

            this.Save(activity);
        }

        public void DeclineFree(ItemFree item)
        {
            var activity = new Activity()
            {
                UserIdentifier = item.OwnerIdentifier,
                Type = Reference.Item,
                Text = string.Format("kept '{0}'.", item.ItemTitle),
                ReferenceIdentifier = item.ItemIdentifier,
            };

            this.Save(activity);
        }
        #endregion

        #region Item Request
        public void NewItemRequest(ItemRequest request)
        {
            var activity = new Activity()
            {
                UserIdentifier = request.UserIdentifier,
                Type = Reference.ItemRequest,
                Text = string.Format("wants '{0}'.", request.Title),
                ReferenceIdentifier = request.Identifier,
            };

            this.Save(activity);
        }

        public void UpdatedItemRequest(ItemRequest request)
        {
            var activity = new Activity()
            {
                UserIdentifier = request.UserIdentifier,
                Type = Reference.ItemRequest,
                Text = string.Format("updated '{0}'.", request.Title),
                ReferenceIdentifier = request.Identifier,
            };

            this.Save(activity);
        }

        public void FulfillRequest(ItemRequestFulfill item)
        {
            var activity = new Activity()
            {
                UserIdentifier = item.RequesterIdentifier,
                Type = Reference.ItemRequest,
                Text = string.Format("is offering to help with '{0}'.", item.Title),
                ReferenceIdentifier = item.ItemRequestIdentifier,
            };

            this.Save(activity);
        }

        public void FulfillAccept(ItemRequestFulfill item)
        {
            var activity = new Activity()
            {
                UserIdentifier = item.OwnerIdentifier,
                Type = Reference.ItemRequest,
                Text = string.Format("accepted '{0}'.", item.Title),
                ReferenceIdentifier = item.ItemRequestIdentifier,
            };

            this.Save(activity);
        }

        public void FulfillDecline(ItemRequestFulfill item)
        {
            var activity = new Activity()
            {
                UserIdentifier = item.OwnerIdentifier,
                Type = Reference.ItemRequest,
                Text = string.Format("has rejected '{0}'.", item.Title),
                ReferenceIdentifier = item.ItemRequestIdentifier,
            };

            this.Save(activity);
        }
        #endregion

        #region Trade
        public void TradeRequested(IList<ItemTradeDTO> trades)
        {
            var tradeItems = (from pt in trades group pt by pt.TradeIdentifier into g select new Trade(g)).ToList<Trade>();
            if (null != tradeItems)
            {
                var activity = new Activity()
                {
                    UserIdentifier = tradeItems.First().Requester.Identifier,
                    Type = Reference.None,
                    Text = string.Format("has opened a trade request with {0}", tradeItems.First().Receiver.Name),
                    ReferenceIdentifier = tradeItems.First().TradeIdentifier,
                };

                this.Save(activity);
            }
        }

        public void TradeAccepted(IList<ItemTradeDTO> trades)
        {
            var tradeItems = (from pt in trades group pt by pt.TradeIdentifier into g select new Trade(g)).ToList<Trade>();
            if (null != tradeItems)
            {
                var activity = new Activity()
                {
                    UserIdentifier = tradeItems.First().Requester.Identifier,
                    Type = Reference.None,
                    Text = string.Format("has accepted a trade request with {0}", tradeItems.First().Receiver.Name),
                    ReferenceIdentifier = tradeItems.First().TradeIdentifier,
                };

                //this.Save(activity);
            }
        }

        public void TradeDeclined(IList<ItemTradeDTO> trades)
        {
            var tradeItems = (from pt in trades group pt by pt.TradeIdentifier into g select new Trade(g)).ToList<Trade>();
            if (null != tradeItems)
            {
                var activity = new Activity()
                {
                    UserIdentifier = tradeItems.First().Requester.Identifier,
                    Type = Reference.None,
                    Text = string.Format("has declined a trade request with {0}", tradeItems.First().Receiver.Name),
                    ReferenceIdentifier = tradeItems.First().TradeIdentifier,
                };

                //this.Save(activity);
            }
        }
        #endregion

        #region Rent
        public void RequestRent(ItemRenting rent)
        {
            var activity = new Activity()
            {
                Type = Reference.Item,
                Text = string.Format("has requested to rent '{0}'.", rent.ItemTitle),
                UserIdentifier = rent.RequesterUserIdentifier,
                ReferenceIdentifier = rent.ItemIdentifier,
            };

            this.Save(activity);
        }

        public void RejectRent(ItemRenting rent)
        {
            var activity = new Activity()
            {
                Type = Reference.Item,
                Text = string.Format("has found out that '{0}' is not available.", rent.ItemTitle),
                UserIdentifier = rent.RequesterUserIdentifier,
                ReferenceIdentifier = rent.ItemIdentifier,
            };

            this.Save(activity);
        }

        public void AcceptRent(ItemRenting rent)
        {
            var activity = new Activity()
            {
                Type = Reference.Item,
                Text = string.Format("is now renting '{0}'", rent.ItemTitle),
                UserIdentifier = rent.RequesterUserIdentifier,
                ReferenceIdentifier = rent.ItemIdentifier,
            };

            this.Save(activity);

            activity = new Activity()
            {
                Type = Reference.Item,
                Text = string.Format("is now renting '{0}'", rent.ItemTitle),
                UserIdentifier = rent.OwnerIdentifier,
                ReferenceIdentifier = rent.ItemIdentifier,
            };

            this.Save(activity);
        }

        /// <summary>
        /// Rental Returned
        /// </summary>
        /// <param name="rent"></param>
        public void ReturnRent(ItemRenting rent)
        {
            var activity = new Activity()
            {
                Type = Reference.Item,
                Text = string.Format("has had '{0}' returned", rent.ItemTitle),
                UserIdentifier = rent.RequesterUserIdentifier,
                ReferenceIdentifier = rent.ItemIdentifier,
            };

            this.Save(activity);

            activity = new Activity()
            {
                Type = Reference.User,
                Text = string.Format("has returned '{0}'", rent.ItemTitle),
                UserIdentifier = rent.OwnerIdentifier,
                ReferenceIdentifier = rent.RequesterUserIdentifier,
            };

            this.Save(activity);
        }
        #endregion

        /// <summary>
        /// Save Activity
        /// </summary>
        /// <param name="activity">Activity</param>
        private void Save(Activity activity)
        {
            if (null == activity)
            {
                return;
            }

            if (Guid.Empty == activity.UserIdentifier)
            {
                return;
            }

            var proc = new UserSaveActivity()
            {
                UserIdentifier = activity.UserIdentifier,
                Text = activity.Text,
                Type = (byte)activity.Type,
                ReferenceIdentifier = activity.ReferenceIdentifier,
            };

            try
            {
                proc.ExecuteNonQuery();
            }
            catch
            {
                // For safety, we need logging.
            }
        }
        #endregion
        #endregion
    }
}
