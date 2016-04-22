namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.DataAccessLayer.Facebook;
    using Borentra.DataAccessLayer.Table;
    using Borentra.DataStore;
    using Borentra.Email;
    using Borentra.Email.Template;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Email Core, sending emails to our clients
    /// </summary>
    /// <remarks>
    /// everything in here should happen on a background thread
    /// </remarks>
    public class EmailCore
    {
        #region Members
        /// <summary>
        /// Item Core
        /// </summary>
        private readonly ItemCore itemCore = new ItemCore();

        /// <summary>
        /// Profile Core
        /// </summary>
        private readonly ProfileCore profileCore = new ProfileCore();

        /// <summary>
        /// From Name
        /// </summary>
        private const string fromName = "Borentra";

        /// <summary>
        /// From Email
        /// </summary>
        private const string fromEmail = "noreply@borentra.com";
        #endregion

        #region Methods
        #region Converstation
        /// <summary>
        /// Converstation
        /// </summary>
        /// <param name="latest">Comments</param>
        public void Conversation(Comment latest, ProfileFull toUser)
        {
            var template = new ConversationTemplate()
            {
                Latest = latest,
            };

            try
            {
                var subject = string.Format("[Borentra] Your friend {0} has sent you a message!", latest.FromDisplayName);
                this.SendGeneric(toUser.Email, latest.FromDisplayName, subject, "New Message", template.TransformText());
            }
            catch
            {
            }
        }
        #endregion

        #region Free
        public void FreeRequest(ItemFree free)
        {
            var commentSproc = new GoodsSearchItemActionComment()
            {
                ItemActionIdentifier = free.Identifier,
                UserIdentifier = free.RequesterUserIdentifier,
            };

            try
            {
                free.Item = itemCore.GetItem(free.ItemIdentifier, null, null, true);

                var template = new FreeRequestTemplate()
                {
                    ItemFree = free,
                    User = profileCore.SearchSingle<ProfileFull>(free.OwnerIdentifier, null, null, true),
                    Friend = profileCore.SearchSingle(free.RequesterUserIdentifier, null, null, true),
                    Comments = commentSproc.CallObjects<ItemActionComment>(),
                };

                var subject = string.Format("[Borentra] Your friend {0} would like your item!", template.Friend.Name);
                this.SendGeneric(template.User.Email, template.Friend.Name, subject, "Give Item Away?", template.TransformText());
            }
            catch
            {
            }
        }

        /// <summary>
        /// Free Accepted
        /// </summary>
        /// <param name="free">Item Free</param>
        public void FreeAccepted(ItemFree free)
        {
            var commentSproc = new GoodsSearchItemActionComment()
            {
                ItemActionIdentifier = free.Identifier,
                UserIdentifier = free.RequesterUserIdentifier,
            };

            try
            {
                free.Item = itemCore.GetItem(free.ItemIdentifier, null, null, true);

                var template = new FreeAcceptTemplate()
                {
                    ItemFree = free,
                    User = profileCore.SearchSingle<ProfileFull>(free.RequesterUserIdentifier, null, null, true),
                    Friend = profileCore.SearchSingle(free.OwnerIdentifier, null, null, true),
                    Comments = commentSproc.CallObjects<ItemActionComment>(),
                };


                var subject = string.Format("[Borentra] Your friend {0} will give you an item!", template.Friend.Name);
                this.SendGeneric(template.User.Email, template.Friend.Name, subject, "Free Item Accepted", template.TransformText());
            }
            catch
            {
            }
        }

        /// <summary>
        /// Free Decline
        /// </summary>
        /// <param name="free">Free</param>
        public void FreeDeclined(ItemFree free)
        {
            var commentSproc = new GoodsSearchItemActionComment()
            {
                ItemActionIdentifier = free.Identifier,
                UserIdentifier = free.RequesterUserIdentifier,
            };

            try
            {
                free.Item = itemCore.GetItem(free.ItemIdentifier, null, null, true);

                var template = new FreeDeclineTemplate()
                {
                    ItemFree = free,
                    User = profileCore.SearchSingle<ProfileFull>(free.RequesterUserIdentifier, null, null, true),
                    Friend = profileCore.SearchSingle(free.OwnerIdentifier, null, null, true),
                    Comments = commentSproc.CallObjects<ItemActionComment>(),
                };

                var subject = string.Format("[Borentra] Your friend {0} is unable to give you their item.", template.Friend.Name);
                this.SendGeneric(template.User.Email, template.Friend.Name, subject, "Free item unavailable", template.TransformText());
            }
            catch
            {
            }
        }
        #endregion

        #region Borrow
        /// <summary>
        /// Borrow Request
        /// </summary>
        public void BorrowRequest(ItemShare share)
        {
            var commentSproc = new GoodsSearchItemActionComment()
            {
                ItemActionIdentifier = share.Identifier,
                UserIdentifier = share.RequesterUserIdentifier,
            };

            try
            {
                share.Item = itemCore.GetItem(share.ItemIdentifier, null, null, true);

                var template = new BorrowRequestTemplate()
                {
                    ItemShare = share,
                    User = profileCore.SearchSingle<ProfileFull>(share.OwnerIdentifier, null, null, true),
                    Friend = profileCore.SearchSingle(share.RequesterUserIdentifier, null, null, true),
                    Comments = commentSproc.CallObjects<ItemActionComment>(),
                };

                var subject = string.Format("[Borentra] Your friend {0} would like to borrow an offer!", template.Friend.Name);
                this.SendGeneric(template.User.Email, template.Friend.Name, subject, "Lend an offer?", template.TransformText());
            }
            catch
            {
            }
        }

        /// <summary>
        /// Borrow Declined
        /// </summary>
        public void RejectBorrow(ItemShare share)
        {
            var commentSproc = new GoodsSearchItemActionComment()
            {
                ItemActionIdentifier = share.Identifier,
                UserIdentifier = share.RequesterUserIdentifier,
            };

            try
            {
                share.Item = itemCore.GetItem(share.ItemIdentifier, null, null, true);

                var template = new BorrowDeclinedTemplate()
                {
                    ItemShare = share,
                    User = profileCore.SearchSingle<ProfileFull>(share.RequesterUserIdentifier, null, null, true),
                    Friend = profileCore.SearchSingle(share.OwnerIdentifier, null, null, true),
                    Comments = commentSproc.CallObjects<ItemActionComment>(),
                };

                var subject = string.Format("[Borentra] Your friend {0} has declined your borrow request", template.Friend.Name);

                this.SendGeneric(template.User.Email, template.Friend.Name, subject, "Unable to Borrow offer", template.TransformText());
            }
            catch
            {
            }
        }

        /// <summary>
        /// Borrow Accepted
        /// </summary>
        public void BorrowAccepted(ItemShare share)
        {
            var commentSproc = new GoodsSearchItemActionComment()
            {
                ItemActionIdentifier = share.Identifier,
                UserIdentifier = share.RequesterUserIdentifier,
            };

            try
            {
                share.Item = itemCore.GetItem(share.ItemIdentifier, null, null, true);

                var template = new BorrowAcceptedTemplate()
                {
                    ItemShare = share,
                    User = profileCore.SearchSingle<ProfileFull>(share.RequesterUserIdentifier, null, null, true),
                    Friend = profileCore.SearchSingle(share.OwnerIdentifier, null, null, true),
                    Comments = commentSproc.CallObjects<ItemActionComment>(),
                };

                var subject = string.Format("[Borentra] Your friend {0} has accepted your borrow request", template.Friend.Name);

                this.SendGeneric(template.User.Email, template.Friend.Name, subject, "Borrow Accepted", template.TransformText());
            }
            catch
            {
            }
        }

        /// <summary>
        /// Borrow Returned
        /// </summary>
        public void BorrowReturned(ItemShare share)
        {
            var commentSproc = new GoodsSearchItemActionComment()
            {
                ItemActionIdentifier = share.Identifier,
                UserIdentifier = share.RequesterUserIdentifier,
            };

            try
            {
                share.Item = itemCore.GetItem(share.ItemIdentifier, null, null, true);

                var template = new BorrowReturnedTemplate()
                {
                    ItemShare = share,
                    User = profileCore.SearchSingle<ProfileFull>(share.RequesterUserIdentifier, null, null, true),
                    Friend = profileCore.SearchSingle(share.OwnerIdentifier, null, null, true),
                    Comments = commentSproc.CallObjects<ItemActionComment>(),
                };

                var subject = string.Format("[Borentra] Your friend {0} has returned your offer", template.Friend.Name);

                this.SendGeneric(template.User.Email, template.Friend.Name, subject, "Offer Returned", template.TransformText());
            }
            catch
            {
            }
        }
        #endregion

        #region Request
        public void Fulfill(ItemRequestFulfill item)
        {
            var commentSproc = new GoodsSearchItemActionComment()
            {
                ItemActionIdentifier = item.Identifier,
                UserIdentifier = item.RequesterIdentifier,
            };

            try
            {
                var template = new ItemRequestFulfillTemplate()
                {
                    Fulfill = item,
                    Comments = commentSproc.CallObjects<ItemActionComment>(),
                };

                var profile = profileCore.SearchSingle<ProfileFull>(item.OwnerIdentifier, null, null, true);

                var title = string.Format("[Borentra] Your friend {0} would like to fulfill your wish!", item.RequesterDisplayName);
                this.SendGeneric(profile.Email, item.OwnerDisplayName, title, string.Empty, template.TransformText());
            }
            catch
            {
            }
        }

        public void FulfillAccept(ItemRequestFulfill item)
        {
            var commentSproc = new GoodsSearchItemActionComment()
            {
                ItemActionIdentifier = item.Identifier,
                UserIdentifier = item.RequesterIdentifier,
            };

            try
            {
                var template = new ItemRequestFulfillAcceptTemplate()
                {
                    Fulfill = item,
                    Comments = commentSproc.CallObjects<ItemActionComment>(),
                };

                var profile = profileCore.SearchSingle<ProfileFull>(item.RequesterIdentifier, null, null, true);

                var title = string.Format("[Borentra] Your friend {0} will fulfill your request!", item.OwnerDisplayName);
                this.SendGeneric(profile.Email, item.RequesterDisplayName, title, string.Empty, template.TransformText());
            }
            catch
            {
            }
        }

        public void FulfillDecline(ItemRequestFulfill item)
        {
            var commentSproc = new GoodsSearchItemActionComment()
            {
                ItemActionIdentifier = item.Identifier,
                UserIdentifier = item.RequesterIdentifier,
            };

            try
            {
                var template = new ItemRequestFulfillDeclineTemplate()
                {
                    Fulfill = item,
                    Comments = commentSproc.CallObjects<ItemActionComment>(),
                };

                var profile = profileCore.SearchSingle<ProfileFull>(item.RequesterIdentifier, null, null, true);

                var title = string.Format("[Borentra] Your friend {0} is unable to fulfill your request.", item.OwnerDisplayName);
                this.SendGeneric(profile.Email, item.RequesterDisplayName, title, string.Empty, template.TransformText());
            }
            catch
            {
            }
        }
        #endregion

        #region New User
        /// <summary>
        /// New User Greeting
        /// </summary>
        /// <param name="profile">Profile</param>
        public void NewUserGreeting(ProfileFull profile)
        {
            if (null == profile)
            {
                throw new ArgumentNullException("profile");
            }

            try
            {
                var template = new NewUser()
                {
                    User = profile,
                };

                this.SendGeneric(profile.Email, fromName, "Welcome to Borentra!", "Welcome to Borentra!", template.TransformText());
            }
            catch
            {
            }
        }
        #endregion

        #region Friends
        /// <summary>
        /// Friend Signed Up
        /// </summary>
        /// <param name="friend">Friend</param>
        public void FriendSignedUp(FriendEmail friend)
        {
            var template = new FriendSignedUpTemplate()
            {
                Friend = friend,
            };

            try
            {
                var subject = string.Format("Your friend {0} has joined Borentra.", template.Friend.FriendDisplayName);
                this.SendGeneric(template.Friend.UserEmail, fromName, subject, "Your friend has joined Borentra", template.TransformText());
            }
            catch
            {
            }
        }
        /// <summary>
        /// Friend Signed Up
        /// </summary>
        /// <param name="friend">Friend</param>
        public void NewFriend(FriendEmail friend)
        {
            var template = new NewFriendAdded()
            {
                Friend = friend,
            };

            try
            {
                var subject = string.Format("Your friend {0} is on Borentra.", template.Friend.FriendDisplayName);
                this.SendGeneric(template.Friend.UserEmail, fromName, subject, "Your friend is on Borentra", template.TransformText());
            }
            catch
            {
            }
        }

        public void FriendNewItem(Item item, Profile me, IEnumerable<Profile> friends)
        {
            //foreach(var friend in friends)
            //{
            //    try
            //    {
            //        var template = new FriendAddedItemTemplate()
            //        {
            //            Item = item,
            //            Me = me,
            //            Friend = friend
            //        };

            //        var subject = string.Format("Your friend {0} added a new item!", me.Name);
            //        var body = template.TransformText();
            //        this.SendGeneric(friend.Email, fromEmail, fromName, subject, body);
            //    }
            //    catch
            //    {
            //    }
            //}
        }
        #endregion

        #region Trade
        public void TradeAccepted(Trade trade)
        {
            var template = new TradeAcceptedTemplate()
            {
                Trade = trade
            };

            try
            {
                var subject = string.Format("Your friend {0} has accepted your Trade offer.", template.Trade.Receiver.Name);
                //this.SendGeneric(template.Trade.Requester.Email, fromName, subject, "Your friend has accepted the Trade offer", template.TransformText());
            }
            catch
            {
            }
        }

        public void TradeDeclined(Trade trade)
        {
            var template = new TradeDeclinedTemplate()
            {
                Trade = trade
            };

            try
            {
                var subject = string.Format("Your friend {0} has declined your Trade offer.", template.Trade.Receiver.Name);
                //this.SendGeneric(template.Trade.Requester.Email, fromName, subject, "Your friend has declined the Trade offer", template.TransformText());
            }
            catch
            {
            }
        }

        public void TradeRequest(Trade trade)
        {
            var template = new TradeRequestTemplate()
            {
                Trade = trade
            };

            try
            {
                var subject = string.Format("Your friend {0} is making a Trade offer.", template.Trade.Requester.Name);
                this.SendGeneric(template.Trade.Receiver.Email, fromName, subject, "Your friend wants to trade with you", template.TransformText());
            }
            catch
            {
            }
        }
        #endregion

        #region Rent
        /// <summary>
        /// Rent Request
        /// </summary>
        public void RentRequest(ItemRenting rent)
        {
            var commentSproc = new GoodsSearchItemActionComment()
            {
                ItemActionIdentifier = rent.Identifier,
                UserIdentifier = rent.RequesterUserIdentifier,
            };

            try
            {
                rent.Item = itemCore.GetItem(rent.ItemIdentifier, null, null, true);

                var template = new RentRequestTemplate()
                {
                    Rent = rent,
                    User = profileCore.SearchSingle<ProfileFull>(rent.OwnerIdentifier, null, null, true),
                    Friend = profileCore.SearchSingle(rent.RequesterUserIdentifier, null, null, true),
                    Comments = commentSproc.CallObjects<ItemActionComment>(),
                };

                var subject = string.Format("[Borentra] Your friend {0} would like to rent an item!", template.Friend.Name);
                this.SendGeneric(template.User.Email, template.Friend.Name, subject, "Rent an item?", template.TransformText());
            }
            catch
            {
            }
        }

        /// <summary>
        /// Rent Declined
        /// </summary>
        public void RejectRent(ItemRenting rent)
        {
            var commentSproc = new GoodsSearchItemActionComment()
            {
                ItemActionIdentifier = rent.Identifier,
                UserIdentifier = rent.RequesterUserIdentifier,
            };

            try
            {
                rent.Item = itemCore.GetItem(rent.ItemIdentifier, null, null, true);

                var template = new RentDeclinedTemplate()
                {
                    Rent = rent,
                    User = profileCore.SearchSingle<ProfileFull>(rent.RequesterUserIdentifier, null, null, true),
                    Friend = profileCore.SearchSingle(rent.OwnerIdentifier, null, null, true),
                    Comments = commentSproc.CallObjects<ItemActionComment>(),
                };

                var subject = string.Format("[Borentra] Your friend {0} has declined your rent request", template.Friend.Name);

                this.SendGeneric(template.User.Email, template.Friend.Name, subject, "Unable to Rent item", template.TransformText());
            }
            catch
            {
            }
        }

        /// <summary>
        /// Rent Accepted
        /// </summary>
        public void RentAccepted(ItemRenting rent)
        {
            var commentSproc = new GoodsSearchItemActionComment()
            {
                ItemActionIdentifier = rent.Identifier,
                UserIdentifier = rent.RequesterUserIdentifier,
            };

            try
            {
                rent.Item = itemCore.GetItem(rent.ItemIdentifier, null, null, true);

                var template = new RentAcceptedTemplate()
                {
                    Rent = rent,
                    User = profileCore.SearchSingle<ProfileFull>(rent.RequesterUserIdentifier, null, null, true),
                    Friend = profileCore.SearchSingle(rent.OwnerIdentifier, null, null, true),
                    Comments = commentSproc.CallObjects<ItemActionComment>(),
                };

                var subject = string.Format("[Borentra] Your friend {0} has accepted your rent request", template.Friend.Name);

                this.SendGeneric(template.User.Email, template.Friend.Name, subject, "Rent Accepted", template.TransformText());
            }
            catch
            {
            }
        }

        /// <summary>
        /// Rent Returned
        /// </summary>
        public void RentReturned(ItemRenting rent)
        {
            var commentSproc = new GoodsSearchItemActionComment()
            {
                ItemActionIdentifier = rent.Identifier,
                UserIdentifier = rent.RequesterUserIdentifier,
            };

            try
            {
                rent.Item = itemCore.GetItem(rent.ItemIdentifier, null, null, true);

                var template = new RentReturnedTemplate()
                {
                    Rent = rent,
                    User = profileCore.SearchSingle<ProfileFull>(rent.RequesterUserIdentifier, null, null, true),
                    Friend = profileCore.SearchSingle(rent.OwnerIdentifier, null, null, true),
                    Comments = commentSproc.CallObjects<ItemActionComment>(),
                };

                var subject = string.Format("[Borentra] Your friend {0} has returned your offer", template.Friend.Name);

                this.SendGeneric(template.User.Email, template.Friend.Name, subject, "Offer Returned", template.TransformText());
            }
            catch
            {
            }
        }
        #endregion

        #region Social
        public void NewsFeedComment(SocialComment comment)
        {


            //try
            //{
            //    var subject = string.Format("Your friend {0} is making a Trade offer.", template.Trade.Requester.Name);
            //    this.SendGeneric(template.Trade.Receiver.Email, fromName, subject, "Your friend wants to trade with you", template.TransformText());
            //}
            //catch
            //{
            //}
        }
        #endregion

        #region Send
        /// <summary>
        /// This is where we should do the actual email send to the user.
        /// </summary>
        /// <param name="to">To Email Address</param>
        /// <param name="from">From Email Address</param>
        /// <param name="subject">Title</param>
        /// <param name="body">Body</param>
        private void Send(string to, string from, string fromName, string subject, string body)
        {
            if (string.IsNullOrWhiteSpace(from))
            {
                throw new ArgumentException("from");
            }

            if (!string.IsNullOrWhiteSpace(to))
            {
                var sent = false;
                try
                {
                    var email = new MailGun();
                    var response = email.Send(from, fromName, to, subject, body);
                    sent = true;
                    //sent = response.StatusCode == System.Net.HttpStatusCode.OK;
                }
                catch
                {
                }

                this.Archive(from, fromName, to, subject, body, sent);
            }
        }

        private void Archive(string from, string fromName, string to, string subject, string body, bool sent)
        {
            try
            {
                var core = new TableStorage("emailarchive", StorageAccounts.Administrative1);
                var data = new EmailArchiveEntry()
                {
                    From = from,
                    FromName = fromName,
                    To = to,
                    Subject = subject,
                    Body = body,
                    Sent = sent,
                };
                data.GenerateKeys();

                core.InsertOrReplace(data);
            }
            catch
            {
            }
        }
        #endregion

        #region Newsletter
        public void NewsLetter(IEnumerable<Profile> profiles, string title, string body)
        {
            if (null == profiles)
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException();
            }

            if (string.IsNullOrWhiteSpace(body))
            {
                throw new ArgumentException();
            }

            foreach (var profile in profiles)
            {
                if (!string.IsNullOrWhiteSpace(profile.Email))
                {
                    try
                    {
                        this.SendGeneric(profile.Email, fromName, title, title, body);
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// Send Generic
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="fromName">From Name</param>
        /// <param name="subject">Subject</param>
        /// <param name="title">Email Title</param>
        /// <param name="body">Email Body</param>
        public void SendGeneric(string email, string fromName, string subject, string title, string body)
        {
            var template = new GenericTemplate()
            {
                Title = title,
                Body = body,
            };

            try
            {
                this.Send(email, fromEmail, fromName, subject, template.TransformText());
            }
            catch
            {
            }
        }
        #endregion
        #endregion
    }
}