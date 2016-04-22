namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Free Core
    /// </summary>
    public class FreeCore
    {
        #region Members
        /// <summary>
        /// Activity Core
        /// </summary>
        private readonly ActivityCore activityCore = new ActivityCore();

        /// <summary>
        /// Email Core
        /// </summary>
        private readonly EmailCore emailCore = new EmailCore();
        #endregion

        #region Methods
        /// <summary>
        /// Make Free Request
        /// </summary>
        /// <param name="userIdentifier">User Identifier</param>
        /// <param name="identifier">Identifier</param>
        /// <param name="comment">Comment</param>
        /// <returns>Result</returns>
        public ItemFree Request(Guid userIdentifier, Guid itemIdentifier, string comment = null)
        {
            if (Guid.Empty == userIdentifier)
            {
                throw new ArgumentException("userIdentifier");
            }

            if (Guid.Empty == itemIdentifier)
            {
                throw new ArgumentException("itemIdentifier");
            }

            var sproc = new GoodsSaveItemFree()
            {
                UserIdentifier = userIdentifier,
                ItemIdentifier = itemIdentifier,
                Status = (byte?)FreeStatus.Pending,
                Comment = comment,
            };

            var free = sproc.CallObject<ItemFree>();
            if (null != free)
            {
                this.emailCore.FreeRequest(free);
                this.activityCore.RequestFree(free);
            }

            return free;
        }

        /// <summary>
        /// Accept Free Request
        /// </summary>
        /// <param name="userIdentifier">User Identifier</param>
        /// <param name="identifier">Identifier</param>
        /// <param name="comment">Comment</param>
        /// <returns>Result</returns>
        public ItemFree Accept(Guid userIdentifier, Guid identifier, string comment = null)
        {
            if (Guid.Empty == userIdentifier)
            {
                throw new ArgumentException("userIdentifier");
            }

            if (Guid.Empty == identifier)
            {
                throw new ArgumentException("identifier");
            }

            var sproc = new GoodsSaveItemFree()
            {
                Identifier = identifier,
                UserIdentifier = userIdentifier,
                Status = (byte?)FreeStatus.Accepted,
                Comment = comment,
            };

            var free = sproc.CallObject<ItemFree>();
            if (null != free)
            {
                this.emailCore.FreeAccepted(free);
                this.activityCore.AcceptFree(free);
            }

            return free;
        }

        /// <summary>
        /// Decline Free Request
        /// </summary>
        /// <param name="userIdentifier">User Identifier</param>
        /// <param name="identifier">Identifier</param>
        /// <param name="comment">Comment</param>
        /// <returns>Result</returns>
        public ItemFree Decline(Guid userIdentifier, Guid identifier, string comment = null)
        {
            if (Guid.Empty == userIdentifier)
            {
                throw new ArgumentException("userIdentifier");
            }

            if (Guid.Empty == identifier)
            {
                throw new ArgumentException("identifier");
            }

            var sproc = new GoodsSaveItemFree()
            {
                UserIdentifier = userIdentifier,
                Identifier = identifier,
                Status = (byte?)FreeStatus.Declined,
                Comment = comment,
            };

            var free = sproc.CallObject<ItemFree>();
            if (null != free)
            {
                this.emailCore.FreeDeclined(free);
                this.activityCore.DeclineFree(free);
            }

            return free;
        }

        /// <summary>
        /// Cancel Free Request
        /// </summary>
        /// <param name="userIdentifier">User Identifier</param>
        /// <param name="identifier">Identifier</param>
        public void Cancel(Guid userIdentifier, Guid identifier)
        {
            if (Guid.Empty == userIdentifier)
            {
                throw new ArgumentException("userIdentifier");
            }

            if (Guid.Empty == identifier)
            {
                throw new ArgumentException("identifier");
            }

            var sproc = new GoodsSaveItemFree()
            {
                UserIdentifier = userIdentifier,
                Identifier = identifier,
                Delete = true,
            };

            sproc.ExecuteNonQuery();
        }

        /// <summary>
        /// Search Item Free
        /// </summary>
        /// <param name="ownerId">Owner Identifier</param>
        /// <param name="requesterId">Requester Identifier</param>
        /// <returns>Item Free's</returns>
        public IEnumerable<ItemFree> Search(Guid? ownerId = null, Guid? requesterId = null)
        {
            ownerId = ownerId.HasValue && ownerId.Value == Guid.Empty ? (Guid?)null : ownerId;
            requesterId = requesterId.HasValue && requesterId.Value == Guid.Empty ? (Guid?)null : requesterId;

            if (!ownerId.HasValue && !requesterId.HasValue)
            {
                throw new ArgumentException("No Search Criteria");
            }

            var sproc = new GoodsSearchItemFree()
            {
                OwnerIdentifier = ownerId,
                RequesterIdentifier = requesterId,
            };

            return sproc.CallObjects<ItemFree>();
        }
        #endregion
    }
}