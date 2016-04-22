namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Item Core
    /// </summary>
    public class ItemCore
    {
        #region Members
        /// <summary>
        /// Administrator Identifier
        /// </summary>
        public static readonly Guid AdministratorIdentifier = Guid.Parse("BA8B47BD-63CE-40B7-B68E-03D2320AA279");

        /// <summary>
        /// Activity Core
        /// </summary>
        private readonly ActivityCore activityCore = new ActivityCore();
        #endregion

        #region Methods
        public IEnumerable<Item> Search(Guid? user, string s = null, string key = null, short? top = null, Guid? callerId = null, bool isAdmin = false, bool isFilterByFriends = false)
        {
            var offer = OfferType.Unknown;
            if (!string.IsNullOrWhiteSpace(s))
            {
                var type = s.ToLowerInvariant();
                switch (type)
                {
                    case "free":
                        offer = OfferType.Free;
                        s = null;
                        break;
                    case "share":
                        offer = OfferType.Share;
                        s = null;
                        break;
                    case "trade":
                        offer = OfferType.Trade;
                        s = null;
                        break;
                    case "rent":
                        offer = OfferType.Rent;
                        s = null;
                        break;
                }
            }

            return Search<Item>(user, s, top, callerId, isAdmin, offer, isFilterByFriends);
        }

        public IEnumerable<Item> Search(Guid? user, OfferType offer, string s = null, short? top = null, Guid? callerId = null, bool isFilterByFriends = false)
        {
            return this.Search<Item>(user, s, top, callerId, false, offer, isFilterByFriends);
        }

        public IEnumerable<T> Rss<T>(byte resultType = 0, bool withImages = false)
            where T : new()
        {
            var sproc = new GoodsSearchForRss()
            {
                ResultType = resultType,
                WithImages = withImages,
            };

            return sproc.CallObjects<T>();
        }

        public IEnumerable<T> Search<T>(Guid? user, string s = null, short? top = null, Guid? callerId = null, bool isAdmin = false, OfferType offer = OfferType.Unknown, bool isFilterByFriends = false)
            where T : new()
        {
            callerId = !callerId.HasValue || callerId.Value == Guid.Empty ? (Guid?)null : callerId.Value;
            user = !user.HasValue || user.Value == Guid.Empty ? (Guid?)null : user.Value;
            var offerType = offer == OfferType.Unknown ? (byte?)null : (byte?)offer;

            var sp = new GoodsSearchItem()
            {
                UserIdentifier = user,
                Keyword = string.IsNullOrWhiteSpace(s) ? null : s,
                Top = top,
                CallerIdentifier = isAdmin ? AdministratorIdentifier : callerId,
                ShareType = offerType,
                FilterFriendsOnly = isFilterByFriends
            };

            return sp.CallObjects<T>();
        }

        /// <summary>
        /// Search Single
        /// </summary>
        /// <param name="id"></param>
        /// <param name="key"></param>
        /// <param name="callerId"></param>
        /// <param name="isAdmin">Use to overload profile privacy</param>
        /// <returns></returns>
        public Item GetItem(Guid? id, string key = null, Guid? callerId = null, bool isAdmin = false)
        {
            callerId = !callerId.HasValue || callerId.Value == Guid.Empty ? (Guid?)null : callerId.Value;
            id = !id.HasValue || id.Value == Guid.Empty ? (Guid?)null : id.Value;

            var sp = new GoodsGetItem()
            {
                Identifier = id,
                Key = string.IsNullOrWhiteSpace(key) ? null : key,
                CallerIdentifier = isAdmin ? AdministratorIdentifier : callerId,
            };

            return sp.Execute().LoadObject<Item>();
        }

        /// <summary>
        /// Save Item
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>Item</returns>
        public Item Save(Item item)
        {
            if (null == item)
            {
                throw new ArgumentNullException("item");
            }

            if (Guid.Empty == item.UserIdentifier)
            {
                throw new ArgumentException("User Identifier");
            }
            
            var sp = new GoodsSaveItem()
            {
                UserIdentifier = item.UserIdentifier.ToNullable(),
                Identifier = item.Identifier.ToNullable(),
                Title = item.Title.TrimIfNotNull(),
                Description = item.Description.TrimIfNotNull(),
                Delete = item.Delete,
                SharePrivacyLevel = item.SharePrivacyLevel == PrivacyLevel.Unknown ? (byte?)null : (byte?)item.SharePrivacyLevel,
                RentPrivacyLevel = item.RentPrivacyLevel == PrivacyLevel.Unknown ? (byte?)null : (byte?)item.RentPrivacyLevel,
                TradePrivacyLevel = item.TradePrivacyLevel == PrivacyLevel.Unknown ? (byte?)null : (byte?)item.TradePrivacyLevel,
                FreePrivacyLevel = item.FreePrivacyLevel == PrivacyLevel.Unknown ? (byte?)null : (byte?)item.FreePrivacyLevel,
            };

            var data = sp.Execute().LoadObject<Item>();

            if (null != data)
            {
                if (item.Identifier != data.Identifier && data.IsNew)
                {
                    activityCore.NewItem(data);
                }
                else
                {
                    activityCore.UpdateItem(data);
                }

                if (item.RentPrivacyLevel != PrivacyLevel.Private
                    && item.RentPrivacyLevel != PrivacyLevel.Unknown
                    && item.PerUnit == RentalUnit.PerDay
                    && 0 < item.Price)
                {
                    var rent = new ItemRent()
                    {
                        ItemIdentifier = item.Identifier,
                        Price = item.Price,
                        PerUnit = item.PerUnit,
                    };

                    var rentCore = new RentCore();
                    var price = rentCore.SetPrice(rent, item.UserIdentifier);

                    if (null != price)
                    {
                        data.PerUnit = price.PerUnit;
                        data.Price = price.Price;
                    }
                }
            }

            return data;
        }

        /// <summary>
        /// Item Base Url
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Base Url</returns>
        public static string BaseUrl(string key)
        {
            return string.Format("/offer/{0}", key.TrimIfNotNull());
        }
        /// <summary>
        /// Offer Search Url
        /// </summary>
        /// <param name="term">Key</param>
        /// <returns>Search Url</returns>
        public static string SearchUrl(string term, string category = "organic")
        {
            return string.Format("/search/offer?s={0}&c={1}", term.TrimIfNotNull(), category.TrimIfNotNull());
        }
        #endregion
    }
}