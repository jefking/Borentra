namespace Borentra.Models
{
    using System;

    public class ItemImageMetaData : IIdentifier
    {
        #region Properties
        public Guid Identifier
        {
            get;
            set;
        }

        public bool IsPrimary
        {
            get;
            set;
        }

        public bool Delete
        {
            get;
            set;
        }
        #endregion
    }
}