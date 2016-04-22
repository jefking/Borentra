namespace Borentra.Models
{
    using Borentra.Models.DataTransferObjects;
    using System.Collections.Generic;

    public class MyProfile : Profile
    {
        #region Properties
        public IEnumerable<ItemRental> RentAsks
        {
            get;
            set;
        }
        public IEnumerable<ItemRental> RentRequests
        {
            get;
            set;
        }
        public IEnumerable<ItemRental> Rented
        {
            get;
            set;
        }
        public IEnumerable<ItemRental> Renting
        {
            get;
            set;
        }
        public IEnumerable<ItemShare> BorrowRequests
        {
            get;
            set;
        }

        public IEnumerable<ItemShare> PendingRequests
        {
            get;
            set;
        }

        public IEnumerable<ItemShare> Borrowed
        {
            get;
            set;
        }

        public IEnumerable<ItemShare> Lent
        {
            get;
            set;
        }

        public IEnumerable<ItemFree> FreeRequested
        {
            get;
            set;
        }

        public IEnumerable<ItemFree> FreeAsk
        {
            get;
            set;
        }

        public IEnumerable<Trade> PendingTradeRequests
        { 
            get; 
            set; 
        }

        public IEnumerable<Trade> TradeRequests
        {
            get;
            set;
        }

        public IEnumerable<Trade> TradeHistory
        { 
            get; 
            set; 
        }

        public IEnumerable<ItemRequestFulfill> FulfillMine
        {
            get;
            set;
        }

        public IEnumerable<ItemRequestFulfill> FulfillOthers
        {
            get;
            set;
        }

        public string Theme
        {
            get;
            set;
        }
        #endregion
    }
}