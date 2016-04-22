namespace Borentra.Models
{
    using System;

    public class MobileContact : IUserIdentifier
    {
        #region Properties
        public Guid UserIdentifier
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public bool Invite
        {
            get;
            set;
        }
        public string PhoneNumber
        {
            get;
            set;
        }
        #endregion
    }
}
