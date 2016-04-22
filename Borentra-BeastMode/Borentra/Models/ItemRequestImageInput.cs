namespace Borentra.Models
{
    using Borentra.Models;
    using System;

    public class ItemRequestImageInput : ImageInput
    {
        #region Properties
        public Guid ItemRequestIdentifier
        {
            get;
            set;
        }
        #endregion
    }
}