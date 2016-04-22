namespace Borentra.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Profile Master
    /// </summary>
    public class ProfileMaster : Master<Profile>
    {
        #region Properties
        /// <summary>
        /// Friends
        /// </summary>
        public IEnumerable<Profile> Friends
        {
            get;
            set;
        }

        /// <summary>
        /// Item Requests
        /// </summary>
        public IEnumerable<ItemRequest> ItemRequests
        {
            get;
            set;
        }

        /// <summary>
        /// Items Lent
        /// </summary>
        public IEnumerable<ItemShare> Lent
        {
            get;
            set;
        }

        /// <summary>
        /// Items Lent
        /// </summary>
        public IEnumerable<ItemShare> Borrowed
        {
            get;
            set;
        }
        #endregion
    }
}