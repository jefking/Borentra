namespace Borentra.GeoSpatial
{
    using Borentra.DataAccessLayer;
    using Borentra.GeoSpatial;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;

    /// <summary>
    /// Extension Methods
    /// </summary>
    public static class ExtensionMethods
    {
        #region ILocation
        public static Coordinate GetCoordinate(this ILocation location)
        {
            return new Coordinate()
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
            };
        }
        #endregion

        #region Coordinate
        public static Coordinate MoveTo(this Coordinate source, double range, double bearing)
        {
            double latA = source.Latitude * GeoConstants.DegreesToRadians;
            double lonA = source.Longitude * GeoConstants.DegreesToRadians;
            double angularDistance = range / GeoConstants.EarthRadius;
            double trueCourse = bearing * GeoConstants.DegreesToRadians;

            double lat = Math.Asin(Math.Sin(latA) * Math.Cos(angularDistance) +
                Math.Cos(latA) * Math.Sin(angularDistance) * Math.Cos(trueCourse));

            double dlon = Math.Atan2(Math.Sin(trueCourse) * Math.Sin(angularDistance) * Math.Cos(latA),
                Math.Cos(angularDistance) - Math.Sin(latA) * Math.Sin(lat));

            double lon = ((lonA + dlon + Math.PI) % GeoConstants.TwoPi) - Math.PI;

            return new Coordinate()
            {
                Latitude = lat * GeoConstants.RadiansToDegrees,
                Longitude = lon * GeoConstants.RadiansToDegrees,
            };
        }
        #endregion
    }
}