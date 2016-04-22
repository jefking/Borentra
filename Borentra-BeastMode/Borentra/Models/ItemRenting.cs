namespace Borentra.Models
{
    using System;

    /// <summary>
    /// Item Renting
    /// </summary>
    public class ItemRenting : ItemAction
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
        #endregion
    }
}