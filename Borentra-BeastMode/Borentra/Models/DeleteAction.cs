namespace Borentra.Models
{
    using System;

    public class DeleteAction : IIdentifier
    {
        #region Properties
        public Guid Identifier
        {
            get;
            set;
        }
        #endregion
    }
}