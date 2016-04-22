namespace Borentra.Models
{
    /// <summary>
    /// Location Search
    /// </summary>
    public class LocationSearch : ILocation
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
        #endregion
    }
}