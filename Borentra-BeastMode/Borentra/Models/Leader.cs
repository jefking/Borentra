namespace Borentra.Models
{
    using Borentra.Core;
    using System;

    public class Leader : IUserIdentifier, IKey, IFacebookIdentity
    {
        #region Properties
        public Guid UserIdentifier
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }
        public long FacebookId
        {
            get;
            set;
        }

        public Uri Picture
        {
            get
            {
                return this.Picture();
            }
        }

        public string DisplayName
        {
            get;
            set;
        }
        public int Points
        {
            get;
            set;
        }
        #endregion
    }
}