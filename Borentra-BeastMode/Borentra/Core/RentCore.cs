namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Rent Core
    /// </summary>
    public class RentCore
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
        public ItemRenting Request(RentalRequest rent, Guid userIdentifier)
        {
            if (null == rent)
            {
                throw new ArgumentNullException("rent");
            }

            if (Guid.Empty == userIdentifier)
            {
                throw new ArgumentException("user identifier");
            }

            if (Guid.Empty == rent.ItemIdentifier)
            {
                throw new ArgumentException("item identifier");
            }

            var sproc = new GoodsRentRequest()
            {
                ItemIdentifier = rent.ItemIdentifier,
                UserIdentifier = userIdentifier,
                On = rent.On,
                Until = rent.Until,
                Comment = rent.Comment,
            };

            var data = sproc.CallObject<ItemRenting>();

            if (null != data)
            {
                this.emailCore.RentRequest(data);
                this.activityCore.RequestRent(data);
            }

            return data;
        }

        public ItemRenting Reject(RentalAction rent, Guid userIdentifier)
        {
            if (null == rent)
            {
                throw new ArgumentNullException("rent");
            }

            if (Guid.Empty == userIdentifier)
            {
                throw new ArgumentException("user identifier");
            }

            var sproc = new GoodsRentReject()
            {
                CallerIdentifier = userIdentifier,
                Identifier = rent.Identifier,
                Comment = rent.Comment,
            };

            var data = sproc.CallObject<ItemRenting>();

            if (null != data)
            {
                this.emailCore.RejectRent(data);
                this.activityCore.RejectRent(data);
            }

            return data;
        }

        public ItemRenting Return(RentalReturned rent, Guid userIdentifier)
        {
            if (null == rent)
            {
                throw new ArgumentNullException("rent");
            }

            if (Guid.Empty == userIdentifier)
            {
                throw new ArgumentException("user identifier");
            }

            var sproc = new GoodsRentReturn()
            {
                CallerIdentifier = userIdentifier,
                Identifier = rent.Identifier,
                ReturnedOn = rent.ReturnedOn,
                Comment = rent.Comment,
            };

            var data = sproc.CallObject<ItemRenting>();

            if (null != data)
            {
                this.emailCore.RentReturned(data);
                this.activityCore.ReturnRent(data);
            }

            return data;
        }

        public ItemRenting Accept(RentalAction rent, Guid userIdentifier)
        {
            if (null == rent)
            {
                throw new ArgumentNullException("rent");
            }

            if (Guid.Empty == userIdentifier)
            {
                throw new ArgumentException("user identifier");
            }

            var sproc = new GoodsRentAccept()
            {
                CallerIdentifier = userIdentifier,
                Identifier = rent.Identifier,
                Comment = rent.Comment,
            };

            var data = sproc.CallObject<ItemRenting>();

            if (null != data)
            {
                this.emailCore.RentAccepted(data);
                this.activityCore.AcceptRent(data);
            }

            return data;
        }

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

            var sproc = new GoodsRentDelete()
            {
                Identifier = delete.Identifier,
                CallerIdentifier = userIdentifier,
            };

            sproc.ExecuteNonQuery();
        }

        /// <summary>
        /// Set Item Price for Rent
        /// </summary>
        /// <param name="item">Item Rent</param>
        /// <param name="userId">user Id</param>
        /// <returns>Item Rent</returns>
        public ItemRent SetPrice(ItemRent item, Guid userId)
        {
            if (null == item)
            {
                throw new ArgumentNullException("item");
            }

            if (Guid.Empty == userId)
            {
                throw new ArgumentException("userId");
            }

            if (item.Price <= 0)
            {
                throw new ArgumentException("price");
            }

            if (item.PerUnit != RentalUnit.PerDay)
            {
                throw new ArgumentException("unit");
            }

            var itemIdentifier = item.ItemIdentifier.ToNullable();

            if (!itemIdentifier.HasValue)
            {
                throw new ArgumentException("identifier's");
            }

            var sproc = new GoodsSaveItemRent()
            {
                UserIdentifier = userId,
                ItemIdentifier = itemIdentifier,
                PerUnit = (byte)item.PerUnit,
                Price = item.Price,
            };

            return sproc.CallObject<ItemRent>();
        }

        public IEnumerable<ItemRental> Rented(Guid userId)
        {
            if (Guid.Empty == userId)
            {
                throw new ArgumentException("user identifier");
            }

            var sproc = new GoodsSearchItemRenting()
            {
                OwnerIdentifier = userId,
            };

            return sproc.CallObjects<ItemRental>();
        }

        public IEnumerable<ItemRental> Requested(Guid userId)
        {
            if (Guid.Empty == userId)
            {
                throw new ArgumentException("user identifier");
            }

            var sproc = new GoodsSearchItemRenting()
            {
                RequesterIdentifier = userId,
            };

            return sproc.CallObjects<ItemRental>();
        }
        #endregion
    }
}