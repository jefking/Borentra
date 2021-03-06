﻿namespace Borentra.Email.Template
{
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System.Collections.Generic;

    public partial class BorrowReturnedTemplate
    {
        #region Properties
        public ItemShare ItemShare
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