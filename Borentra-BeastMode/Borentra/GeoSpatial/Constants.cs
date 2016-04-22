namespace Borentra.GeoSpatial
{
    using System;

    public struct GeoConstants
    {
        #region Members
        /// <summary>
        /// Earth Radius in KM
        /// </summary>
        public const int EarthRadius = 6378137;

        /// <summary>
        /// Degrees to Radians
        /// </summary>
        public const double DegreesToRadians = Math.PI / 180;

        /// <summary>
        /// Two Pi
        /// </summary>
        public const double TwoPi = 2 * Math.PI;

        /// <summary>
        /// Radians to Degrees
        /// </summary>
        public const double RadiansToDegrees = 180 / Math.PI;
        #endregion
    }
}