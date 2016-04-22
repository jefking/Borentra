namespace Borentra.Models
{
    using System;

    /// <summary>
    /// Item Share
    /// </summary>
    public class ItemShare : ItemAction
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

        public BorrowStatus Status
        {
            get;
            set;
        }
        #endregion
    }
}