namespace Borentra.Models
{
    using System;

    public class Social
    {
        #region Properties
        public Guid UserIdentifier
        {
            get;
            set;
        }

        public Guid ReferenceIdentifier
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