namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Item Request Core
    /// </summary>
    public class ItemRequestCore
    {
        #region Members
        /// <summary>
        /// Email Core
        /// </summary>
        private readonly EmailCore emailCore = new EmailCore();

        /// <summary>
        /// Activity Core
        /// </summary>
        private readonly ActivityCore activityCore = new ActivityCore();
        #endregion

        #region Methods
        /// <summary>
        /// Save Item Request
        /// </summary>
        /// <param name="item">Item Request</param>
        /// <returns>Item Request</returns>
        public ItemRequest Save(ItemRequest item)
        {
            if (null == item)
            {
                throw new ArgumentNullException("item");
            }

            if (Guid.Empty == item.UserIdentifier)
            {
                throw new ArgumentException("User Identifier");
            }

            var proc = new GoodsSaveItemRequest()
            {
                Delete = item.Delete,
                Description = item.Description,
                ForFree = item.ForFree,
                ForRent = item.ForRent,
                ForShare = item.ForShare,
                ForTrade = item.ForTrade,
                Identifier = item.Identifier.ToNullable(),
                Title = item.Title,
                UserIdentifier = item.UserIdentifier,
            };

            var data = proc.CallObject<ItemRequest>();

            if (null != data)
            {
                if (Guid.Empty == item.Identifier)
                {
                    activityCore.NewItemRequest(data);
                }
                else
                {
                    activityCore.UpdatedItemRequest(data);
                }
            }

            return data;
        }

        /// <summary>
        /// Search for Item Requests
        /// </summary>
        /// <param name="userId">User Identifier</param>
        /// <param name="callerId">Caller Identifier</param>
        /// <param name="keyword">Search Parameter</param>
        /// <param name="top">Top Number</param>
        /// <returns>Item Requests</returns>
        public IEnumerable<ItemRequest> Search(Guid? userId = null, Guid? callerId = null, string keyword = null, short? top = 100)
        {
            keyword = string.IsNullOrWhiteSpace(keyword) ? null : keyword;
            userId = userId.HasValue && userId.Value == Guid.Empty ? (Guid?)null : userId;
            callerId = callerId.HasValue && callerId.Value == Guid.Empty ? (Guid?)null : callerId;

            var proc = new GoodsSearchItemRequest()
            {
                UserIdentifier = userId,
                CallerIdentifier = callerId,
                Keyword = keyword,
                Top = top,
            };

            return proc.CallObjects<ItemRequest>();
        }

        /// <summary>
        /// Search Fulfill
        /// </summary>
        /// <param name="userId">User Identifier</param>
        /// <returns>Item Request Fulfill</returns>
        public IEnumerable<ItemRequestFulfill> SearchFulfill(Guid userId)
        {
            if (Guid.Empty == userId)
            {
                throw new ArgumentException("User Identifier");
            }

            var proc = new GoodsSearchItemRequestFulfill()
            {
                UserIdentifier = userId,
            };

            return proc.CallObjects<ItemRequestFulfill>();
        }

        /// <summary>
        /// Item Request
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="identifier">Identifier</param>
        /// <param name="callerId">Caller Identifier</param>
        /// <returns>Item Request</returns>
        public ItemRequest Get(string key, Guid? identifier, Guid? callerId = null)
        {
            if (string.IsNullOrWhiteSpace(key) && !identifier.HasValue)
            {
                throw new ArgumentException("key & identifier invalid");
            }

            identifier = identifier.HasValue && identifier.Value == Guid.Empty ? (Guid?)null : identifier;
            callerId = callerId.HasValue && callerId.Value == Guid.Empty ? (Guid?)null : callerId;

            var proc = new GoodsGetItemRequest()
            {
                Key = key.TrimIfNotNull(),
                Identifier = identifier,
                CallerIdentifier = callerId,
            };

            return proc.CallObject<ItemRequest>();
        }

        /// <summary>
        /// Save Item Request Fulfillment
        /// </summary>
        /// <param name="fulfill">Item Request Fulfill</param>
        /// <returns>Item Request Fulfill</returns>
        public ItemRequestFulfill Save(ItemRequestFulfill fulfill)
        {
            if (null == fulfill)
            {
                throw new ArgumentNullException("fulfill");
            }

            if (Guid.Empty == fulfill.ItemRequestIdentifier)
            {
                throw new ArgumentException("Item Request Identifier");
            }

            if (Guid.Empty == fulfill.UserIdentifier)
            {
                throw new ArgumentException("User Identifier");
            }

            var sproc = new GoodsSaveItemRequestFulfill()
            {
                ItemRequestIdentifier = fulfill.ItemRequestIdentifier,
                UserIdentifier = fulfill.UserIdentifier,
                WillGive = fulfill.WillGive,
                WillShare = fulfill.WillShare,
                WillRent = fulfill.WillRent,
                WillTrade = fulfill.WillTrade,
                Comment = fulfill.Comment.TrimIfNotNull(),
                Status = (byte?)fulfill.Status,
            };

            var data = sproc.CallObject<ItemRequestFulfill>();

            if (null != data)
            {
                this.emailCore.Fulfill(data);
                this.activityCore.FulfillRequest(data);
            }

            return data;
        }

        /// <summary>
        /// Decline Item Request Fulfillment
        /// </summary>
        /// <param name="userIdentifier">User Identifier</param>
        /// <param name="identifier">Identifier</param>
        /// <param name="comment">Comment</param>
        /// <returns>Item Request Fulfillment</returns>
        public ItemRequestFulfill Decline(Guid userIdentifier, Guid identifier, string comment = null)
        {
            if (Guid.Empty == userIdentifier)
            {
                throw new ArgumentException("User Identifier");
            }

            if (Guid.Empty == identifier)
            {
                throw new ArgumentException("Identifier");
            }

            var sproc = new GoodsFulfillDecline()
            {
                Identifier = identifier,
                UserIdentifier = userIdentifier,
                Comment = comment.TrimIfNotNull(),
            };

            var data = sproc.CallObject<ItemRequestFulfill>();

            if (null != data)
            {
                this.emailCore.FulfillDecline(data);
                this.activityCore.FulfillDecline(data);
            }

            return data;
        }

        /// <summary>
        /// Accept Item Request Fulfillment
        /// </summary>
        /// <param name="userIdentifier"></param>
        /// <param name="identifier"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public ItemRequestFulfill Accept(Guid userIdentifier, Guid identifier, string comment = null)
        {
            if (Guid.Empty == userIdentifier)
            {
                throw new ArgumentException("User Identifier");
            }

            if (Guid.Empty == identifier)
            {
                throw new ArgumentException("Identifier");
            }

            var sproc = new GoodsFulfillAccept()
            {
                Identifier = identifier,
                UserIdentifier = userIdentifier,
                Comment = comment.TrimIfNotNull(),
            };

            var data = sproc.CallObject<ItemRequestFulfill>();

            if (null != data)
            {
                this.emailCore.FulfillAccept(data);
                this.activityCore.FulfillAccept(data);
            }

            return data;
        }

        /// <summary>
        /// Delete Item Request Fulfillment
        /// </summary>
        /// <param name="userIdentifier"></param>
        /// <param name="identifier"></param>
        public void Delete(Guid userIdentifier, Guid identifier)
        {
            if (Guid.Empty == userIdentifier)
            {
                throw new ArgumentException("User Identifier");
            }

            if (Guid.Empty == identifier)
            {
                throw new ArgumentException("Identifier");
            }

            var sproc = new GoodsFulfillDelete()
            {
                UserIdentifier = userIdentifier,
                Identifier = identifier
            };

            sproc.ExecuteNonQuery();
        }

        /// <summary>
        /// Item Base Url
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Base Url</returns>
        public static string BaseUrl(string key)
        {
            return string.Format("/wanted/{0}", key.TrimIfNotNull());
        }
        /// <summary>
        /// Wanted Search Url
        /// </summary>
        /// <param name="term">Key</param>
        /// <returns>Search Url</returns>
        public static string SearchUrl(string term, string category = "organic")
        {
            return string.Format("/search/wanted?s={0}&c={1}", term.TrimIfNotNull(), category.TrimIfNotNull());
        }
        #endregion
    }
}