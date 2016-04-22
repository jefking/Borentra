namespace Borentra.Core
{
    using Windows.Devices.Geolocation;

    public class ProfileCore
    {
        #region Members
        private readonly ApiCore api = new ApiCore();
        #endregion

        #region Methods
        public async void Update(Geoposition position)
        {
            if (null != position)
            {
                if (null != position.Coordinate && null != position.Coordinate.Point)
                {
                    var pos = position.Coordinate.Point.Position;

                    try
                    {
                        await this.api.SaveProfile(pos.Latitude, pos.Longitude);
                    }
                    catch
                    {

                    }
                }
            }
        }
        #endregion
    }
}