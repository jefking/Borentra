namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Borrow Core
    /// </summary>
    public class BorrowCore
    {
        #region Members
        /// <summary>
        /// Email Core
        /// </summary>
        private readonly EmailCore email = new EmailCore();

        /// <summary>
        /// Activity Core
        /// </summary>
        private readonly ActivityCore activity = new ActivityCore();
        #endregion

        #region Methods
        public ItemShare Request(BorrowRequest borrow, Guid userIdentifier)
        {
            if (null == borrow)
            {
                throw new ArgumentNullException("borrow");
            }

            if (Guid.Empty == userIdentifier)
            {
                throw new ArgumentException("user identifier");
            }

            if (Guid.Empty == borrow.ItemIdentifier)
            {
                throw new ArgumentException("item identifier");
            }

            var sproc = new GoodsBorrow()
            {
                ItemIdentifier = borrow.ItemIdentifier,
                UserIdentifier = userIdentifier,
                On = borrow.On,
                Until = borrow.Until,
                Comment = borrow.Comment,
            };

            var data = sproc.CallObject<ItemShare>();

            if (null != data)
            {
                this.email.BorrowRequest(data);
                this.activity.RequestBorrow(data);
            }

            return data;
        }

        public ItemShare Reject(BorrowAction borrow, Guid userIdentifier)
        {
            if (null == borrow)
            {
                throw new ArgumentNullException("borrow");
            }

            if (Guid.Empty == userIdentifier)
            {
                throw new ArgumentException("user identifier");
            }

            var sproc = new GoodsBorrowReject()
            {
                UserIdentifier = userIdentifier,
                Identifier = borrow.Identifier,
                Comment = borrow.Comment,
            };

            var data = sproc.CallObject<ItemShare>();

            if (null != data)
            {
                this.email.RejectBorrow(data);
                this.activity.RejectBorrow(data);
            }

            return data;
        }

        public ItemShare Return(BorrowReturned borrow, Guid userIdentifier)
        {
            if (null == borrow)
            {
                throw new ArgumentNullException("borrow");
            }

            if (Guid.Empty == userIdentifier)
            {
                throw new ArgumentException("user identifier");
            }

            var sproc = new GoodsBorrowReturn()
            {
                UserIdentifier = userIdentifier,
                Identifier = borrow.Identifier,
                ReturnedOn = borrow.ReturnedOn,
                Comment = borrow.Comment,
            };

            var data = sproc.CallObject<ItemShare>();

            if (null != data)
            {
                this.email.BorrowReturned(data);
                this.activity.ReturnBorrow(data);
            }

            return data;
        }

        /// <summary>
        /// Accept Item Share
        /// </summary>
        /// <param name="borrow">Borrow</param>
        /// <param name="userIdentifier">User Identifier</param>
        /// <returns>Item Share</returns>
        public ItemShare Accept(BorrowAction borrow, Guid userIdentifier)
        {
            if (null == borrow)
            {
                throw new ArgumentNullException("borrow");
            }

            if (Guid.Empty == userIdentifier)
            {
                throw new ArgumentException("user identifier");
            }

            var sproc = new GoodsBorrowAccept()
            {
                UserIdentifier = userIdentifier,
                Identifier = borrow.Identifier,
                On = borrow.On,
                Until = borrow.Until,
                Comment = borrow.Comment,
            };

            var data = sproc.CallObject<ItemShare>();

            if (null != data)
            {
                this.email.BorrowAccepted(data);
                this.activity.AcceptBorrow(data);
            }

            return data;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="delete">Borrow Request</param>
        /// <param name="userIdentifier">User Identifier</param>
        public void Delete(IIdentifier delete, Guid userIdentifier)
        {
            if (null == delete)
            {
                throw new ArgumentNullException("delete");
            }

            if (Guid.Empty == userIdentifier)
            {
                throw new ArgumentException("user identifier");
            }

            if (Guid.Empty == delete.Identifier)
            {
                throw new ArgumentException("delete identifier");
            }

            var sproc = new GoodsBorrowDelete()
            {
                Identifier = delete.Identifier,
                UserIdentifier = userIdentifier,
            };

            sproc.ExecuteNonQuery();
        }

        /// <summary>
        /// Shares By Query
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>Item Shares</returns>
        public IList<ItemShare> Shares(Item item)
        {
            if (null == item)
            {
                throw new ArgumentNullException("item");
            }

            if (Guid.Empty == item.Identifier)
            {
                throw new ArgumentException("Item Identifier");
            }

            var sproc = new GoodsSearchItemShare()
            {
                ItemIdentifier = item.Identifier,
            };

            return sproc.CallObjects<ItemShare>();
        }

        public IEnumerable<ItemShare> Lent(Guid userId)
        {
            if (Guid.Empty == userId)
            {
                throw new ArgumentException("user identifier");
            }

            var sproc = new GoodsSearchItemShare()
            {
                OwnerIdentifier = userId,
            };

            return sproc.CallObjects<ItemShare>();
        }

        public IEnumerable<ItemShare> Borrowed(Guid userId)
        {
            if (Guid.Empty == userId)
            {
                throw new ArgumentException("user identifier");
            }

            var sproc = new GoodsSearchItemShare()
            {
                RequesterIdentifier = userId,
            };

            return sproc.CallObjects<ItemShare>();
        }

        /// <summary>
        /// Recent Item Shares
        /// </summary>
        /// <returns>Item Share</returns>
        public ItemShare Recent()
        {
            return new GoodsRecentItemShare().Execute().LoadObject<ItemShare>();
        }
        #endregion
    }
}