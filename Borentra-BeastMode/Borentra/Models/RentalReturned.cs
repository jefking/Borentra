namespace Borentra.Models
{
    using System;

    public class RentalReturned : IIdentifier, IComment
    {
        #region Properties
        public Guid Identifier
        {
            get;
            set;
        }

        public DateTime? ReturnedOn
        {
            get;
            set;
        }

        public string Comment
        {
            get;
            set;
        }
        #endregion
    }
}