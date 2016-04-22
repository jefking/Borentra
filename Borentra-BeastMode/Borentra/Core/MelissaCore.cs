namespace Borentra.Core
{
    using MelissaData;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.ServiceModel.Channels;
    using System.Web;

    public class MelissaCore
    {
        #region Members
        /// <summary>
        /// API Url
        /// </summary>
        public const string RootUri = "https://api.datamarket.azure.com/Data.ashx/MelissaData/IPCheck/v1/";

        /// <summary>
        /// Account Key
        /// </summary>
        public const string AccountKey = "jiliIP3ZDUIaCCKrbh58qOErKTAcL0k9untZsDG52B0=";

        /// <summary>
        /// Empty Ip Address
        /// </summary>
        private const string EmptyIpAddress = "0.0.0.0";
        #endregion

        #region Methods
        /// <summary>
        /// Remove Safely
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>IP Check Entity</returns>
        public IPCheckEntity ResolveSafely(string ip)
        {
            IPCheckEntity entity = null;
            if (!this.IsPrivateIpAddress(ip))
            {
                try
                {
                    entity = this.Resolve(ip);
                }
                catch (Exception ex)
                {
                    Trace.TraceError(string.Format("{0}", ex.Message));
                }
            }

            return entity;
        }

        public IPCheckEntity Resolve(string ip)
        {
            var client = new IPCheckContainer(new Uri(RootUri));
            client.Credentials = new NetworkCredential("accountKey", AccountKey);
            var marketData = client.SuggestIPAddresses(
                ip,
                5,
                0.7
                ).Execute();

            return marketData.FirstOrDefault();
        }

        public string GetClientIpAddress(IDictionary<string, object> properties)
        {
            const string msHttpContext = "MS_HttpContext";
            if (properties.ContainsKey(msHttpContext))
            {
                return ((HttpContextWrapper)properties[msHttpContext]).Request.UserHostAddress;
            }
            else if (properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                var prop = (RemoteEndpointMessageProperty)properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else
            {
                return EmptyIpAddress;
            }
        }

        public string GetClientIpAddress(HttpRequestBase request)
        {
            try
            {
                var userHostAddress = request.UserHostAddress;

                IPAddress.Parse(userHostAddress);

                var xForwardedFor = request.ServerVariables["X_FORWARDED_FOR"];

                if (string.IsNullOrEmpty(xForwardedFor))
                {
                    return userHostAddress;
                }

                var publicForwardingIps = xForwardedFor.Split(',').Where(ip => !IsPrivateIpAddress(ip)).ToList();

                return publicForwardingIps.Any() ? publicForwardingIps.Last() : userHostAddress;
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("{0}", ex.Message));

                return EmptyIpAddress;
            }
        }

        public bool IsPrivateIpAddress(string ipAddress)
        {
            // http://en.wikipedia.org/wiki/Private_network
            // Private IP Addresses are: 
            //  24-bit block: 10.0.0.0 through 10.255.255.255
            //  20-bit block: 172.16.0.0 through 172.31.255.255
            //  16-bit block: 192.168.0.0 through 192.168.255.255
            //  Link-local addresses: 169.254.0.0 through 169.254.255.255 (http://en.wikipedia.org/wiki/Link-local_address)

            var ip = IPAddress.Parse(ipAddress);
            var octets = ip.GetAddressBytes();

            if (octets[0] == 10)
            {
                return true; // Return to prevent further processing
            }

            if (octets[0] == 172 && octets[1] >= 16 && octets[1] <= 31)
            {
                return true; // Return to prevent further processing
            }

            if (octets[0] == 192 && octets[1] == 168)
            {
                return true; // Return to prevent further processing
            }

            return octets[0] == 169 && octets[1] == 254;
        }
        #endregion
    }
}