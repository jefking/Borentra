namespace Borentra.Models
{
    using System;

    public class RentalRequest : IComment
    {
        #region Properties
        public Guid ItemIdentifier
        {
            get;
            set;
        }

        public DateTime? On
        {
            get;
            set;
        }

        public DateTime? Until
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