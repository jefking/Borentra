namespace Borentra.Email.Template
{
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Free Decline Template Partial
    /// </summary>
    public partial class FreeDeclineTemplate
    {
        #region Properties
        public ItemFree ItemFree
        {
            get;
            set;
        }

        public ProfileFull User
        {
            get;
            set;
        }

        public Profile Friend
        {
            get;
            set;
        }

        public IEnumerable<ItemActionComment> Comments
        {
            get;
            set;
        }
        #endregion
    }
}