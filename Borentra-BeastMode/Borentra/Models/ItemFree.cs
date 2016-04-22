namespace Borentra.Models
{
    using System;

    /// <summary>
    /// Item Free
    /// </summary>
    public class ItemFree : ItemAction
    {
        #region Properties
        /// <summary>
        /// Free Status
        /// </summary>
        public FreeStatus Status
        {
            get;
            set;
        }

        public DateTime On
        {
            get;
            set;
        }
        #endregion
    }
}