namespace Borentra.Models
{
    using System;

    public class Profile
    {
        #region Properties
        public string Location
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }
        public string Status
        {
            get;
            set;
        }
        public Uri Picture
        {
            get;
            set;
        }

        public double Latitude
        {
            get;
            set;
        }
        public double Longitude
        {
            get;
            set;
        }
        #endregion
    }
}
