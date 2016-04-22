namespace Borentra.Models
{
    using System;

    public class BorrowAction : IIdentifier, IComment
    {
        #region Properties
        public Guid Identifier
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