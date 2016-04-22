namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.DataAccessLayer.Admin;
    using Borentra.DataStore;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Geo Core
    /// </summary>
    public class GeoCore
    {
        #region Members
        /// <summary>
        /// Spatial IP Container
        /// </summary>
        private const string spatialIpContainer = "spatialip";

        /// <summary>
        /// Spatial Ip File Format
        /// </summary>
        private const string spatialIpFileFormat = "{0}.bin";
        #endregion

        #region Methods
        /// <summary>
        /// Locations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SearchLocationIp> Locations()
        {
            var proc = new GeoLocationByIpRange();
            return proc.CallObjects<SearchLocationIp>();
        }

        /// <summary>
        /// Store Locations
        /// </summary>
        public async void StoreLocations()
        {
            var locations = this.Locations();
            var dictionary = new Dictionary<long, IList<SearchLocationIp>>();
            foreach (var location in locations)
            {
                var keys = new long[]{this.Floor(location.StartIp), this.Floor(location.EndIp)};
                
                if (keys[0] == keys[1])
                {
                    keys = new long[] { keys[0] };
                }

                foreach (var key in keys)
                {
                    if (dictionary.ContainsKey(key))
                    {
                        dictionary[key].Add(location);
                    }
                    else
                    {
                        var locs = new List<SearchLocationIp>();
                        locs.Add(location);
                        dictionary.Add(key, locs);
                    }
                }
            }

            var container = new BinaryContainer(spatialIpContainer);
            foreach (var key in dictionary.Keys)
            {
                container.Save(string.Format(spatialIpFileFormat, key), dictionary[key].Serialize());
            }
        }

        /// <summary>
        /// Floor long, to standardize location
        /// </summary>
        /// <param name="i">value</param>
        /// <returns>Floored Value</returns>
        private long Floor(long i)
        {
            const int flat = 10000000;
            return ((long)i / flat) * flat;
        }

        /// <summary>
        /// Search for Location
        /// </summary>
        /// <param name="ipAddress">IP Address</param>
        /// <returns>Location</returns>
        public async Task<ILocation> Search(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                throw new ArgumentException("ipAddress");
            }

            var octets = ipAddress.Split('.');

            long ipValue = 16777216 * long.Parse(octets[0]);
            ipValue += 65536 * long.Parse(octets[1]);
            ipValue += 256 * long.Parse(octets[2]);
            ipValue += long.Parse(octets[3]);

            var floored = this.Floor(ipValue);

            var container = new BinaryContainer(spatialIpContainer);
            var data = container.Get(string.Format(spatialIpFileFormat, floored));
            if (null != data)
            {
                var locations = data.Deserialize<List<SearchLocationIp>>();

                return locations.Where(l => l.StartIp <= ipValue && l.EndIp >= ipValue).FirstOrDefault();
            }

            return null;
        }

        public async Task<LocationByIp> GetGeoFromIp(string ipAddress)
        {
            LocationByIp result = null;
            try
            {
                var location = await this.Search(ipAddress);
                result = new LocationByIp()
                {
                    Location = location.Location,
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                    IPAddress = ipAddress,
                };
            }
            catch
            { }

            if (null == result)
            {
                var core = new MelissaCore();
                var location = core.ResolveSafely(ipAddress);
                result = new LocationByIp()
                {
                    Location = location.ToString(),
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                    IPAddress = ipAddress,
                };
            }

            return result;
        }
        #endregion
    }
}