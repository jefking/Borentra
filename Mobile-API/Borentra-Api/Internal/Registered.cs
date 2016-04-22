namespace Borentra.API.Internal
{
    using Borentra.API.Models;
    using Borentra.DataAccessLayer.Facebook;
    using Borentra.Models;
    using System;

    public class Registered : IUserIdentifier
    {
        #region Properties
        public Registration Registration
        {
            get;
            set;
        }

        public MeInfo FacebookInfo
        {
            get;
            set;
        }

        public bool IsFacebookVerified
        {
            get
            {
                return null != this.FacebookInfo;
            }
        }

        public Guid UserIdentifier
        {
            get;
            set;
        }

        public string UserKey
        {
            get
            {
                return this.FacebookInfo.FacebookId.ToString();
            }
        }

        public string IpAddress
        {
            get;
            set;
        }
        #endregion
    }
}