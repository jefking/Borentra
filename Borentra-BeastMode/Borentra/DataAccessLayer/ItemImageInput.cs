namespace Borentra.DataAccessLayer
{
    using Borentra.Models;
    using System;

    public class ItemImageInput : ImageInput
    {
        #region Properties
        public Guid ItemIdentifier
        {
            get;
            set;
        }
        #endregion
    }
}