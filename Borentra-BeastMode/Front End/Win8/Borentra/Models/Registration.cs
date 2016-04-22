using System;

namespace Borentra.Models
{
    public class Registration
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

        public byte OperatingSystem
        {
            get
            {
                return 4;
            }
        }
        #endregion
    }
}
