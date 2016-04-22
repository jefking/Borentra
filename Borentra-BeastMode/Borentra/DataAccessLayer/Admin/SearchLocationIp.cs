namespace Borentra.DataAccessLayer.Admin
{
    using Borentra.Models;
    using System;

    [Serializable]
    public class SearchLocationIp : ILocation
    {
        #region Properties
        public string Location
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
        public long StartIp
        {
            get;
            set;
        }
        public long EndIp
        {
            get;
            set;
        }
        #endregion
    }
}