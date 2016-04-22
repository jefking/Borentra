namespace Borentra.DataAccessLayer
{
    using Borentra.Models;

    public class LocationByIp : ILocation
    {
        #region Properties
        public string Location
        {
            get;
            set;
        }
        public string IPAddress
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