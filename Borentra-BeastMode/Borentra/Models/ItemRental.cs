namespace Borentra.Models
{
    using System;

    public class ItemRental: ItemAction, IRental
    {
        #region Properties
        public DateTime On
        {
            get;
            set;
        }

        public DateTime? Until
        {
            get;
            set;
        }

        public DateTime? ReturnedOn
        {
            get;
            set;
        }

        public RentalStatus Status
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