namespace Borentra.Models
{
    using Borentra.DataAccessLayer;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Item
    /// </summary>
    public class Item : Thing, IRental
    {
        #region Properties
        public bool IsFree
        {
            get;
            set;
        }

        public BorrowStatus Status
        {
            get;
            set;
        }

        public IEnumerable<ItemShare> Shares
        {
            get;
            set;
        }

        public DateTime ModifiedOn
        {
            get;
            set;
        }

        public override bool IsPublic
        {
            get
            {
                return this.FreePrivacyLevel == PrivacyLevel.Public
                    || this.TradePrivacyLevel == PrivacyLevel.Public
                    || this.RentPrivacyLevel == PrivacyLevel.Public
                    || this.SharePrivacyLevel == PrivacyLevel.Public;
            }
        }

        public PrivacyLevel SharePrivacyLevel
        {
            get;
            set;
        }

        public PrivacyLevel TradePrivacyLevel
        {
            get;
            set;
        }

        public PrivacyLevel FreePrivacyLevel
        {
            get;
            set;
        }

        public PrivacyLevel RentPrivacyLevel
        {
            get;
            set;
        }

        public string ExternalReviews 
        { 
            get; 
            set; 
        }

        public decimal Price
        {
            get;
            set;
        }

        public RentalUnit PerUnit
        {
            get;
            set;
        }
        #endregion
    }
}