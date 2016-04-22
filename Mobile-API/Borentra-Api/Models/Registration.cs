namespace Borentra.API.Models
{
    using Borentra.Models;
    using System;

    public class Registration : IFacebookAccess
    {
        #region Properties
        public string FacebookAccessToken
        {
            get;
            set;
        }

        public DateTime FacebookTokenExpiration
        {
            get;
            set;
        }

        public Guid DeviceIdentifier
        {
            get;
            set;
        }

        public MobileOS OperatingSystem
        {
            get;
            set;
        }
        #endregion
    }
}