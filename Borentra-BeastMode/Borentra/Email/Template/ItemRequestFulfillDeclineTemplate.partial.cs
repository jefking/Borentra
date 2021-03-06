﻿namespace Borentra.Email.Template
{
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System.Collections.Generic;

    public partial class ItemRequestFulfillDeclineTemplate
    {
        #region Properties
        public ItemRequestFulfill Fulfill
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