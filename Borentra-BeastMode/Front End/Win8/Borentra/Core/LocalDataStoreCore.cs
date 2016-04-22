namespace Borentra.Core
{
    using Borentra.Models;
    using System;
    using Windows.Storage;

    public class LocalDataStoreCore
    {
        #region Members
        /// <summary>
        /// Application Local Settings Container
        /// </summary>
        private readonly ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        
        /// <summary>
        /// Token Key
        /// </summary>
        private const string TokenKey = "9B49FBCB-7BC6-4922-B7D4-E562A2826A34";
        /// <summary>
        /// Facebook Access Token Key
        /// </summary>
        private const string FacebookAccessTokenKey = "38D8DB6B-E094-4981-8FEE-B39D796873E8";
        /// <summary>
        /// Facebook Profile Id Key
        /// </summary>
        private const string FacebookProfileIdKey = "6E58A451-1474-470A-BFD1-3D03185F3C64";
        /// <summary>
        /// Facebook Profile Id Key
        /// </summary>
        private const string DeviceIdentifierKey = "DA682875-6FBB-4C7F-98CD-C56D555BF5C2";
        #endregion

        #region Methods
        public Guid DeviceIdentifier()
        {
            var value = localSettings.Values[DeviceIdentifierKey];
            if (null == value)
            {
                value = Guid.NewGuid();
                localSettings.Values[DeviceIdentifierKey] = value;
            }

            return (Guid)value;
        }

        public Token AccessToken()
        {
            return new Token()
            {
                AccessToken = localSettings.Values[TokenKey] as string,
            };
        }
        public void AccessToken(Token t)
        {
            if (null != t && !string.IsNullOrWhiteSpace(t.AccessToken))
            {
                this.localSettings.Values[TokenKey] = t.AccessToken;
            }
            else
            {
                this.localSettings.Values[TokenKey] = null;
            }
        }
        public void Set(FacebookInfo info)
        {
            if (null != info)
            {
                this.localSettings.Values[FacebookAccessTokenKey] = string.IsNullOrWhiteSpace(info.AccessToken) ? null : info.AccessToken;
                this.localSettings.Values[FacebookProfileIdKey] = string.IsNullOrWhiteSpace(info.ProfileId) ? null : info.ProfileId;
            }
            else
            {
                this.localSettings.Values[FacebookAccessTokenKey] = null;
                this.localSettings.Values[FacebookProfileIdKey] = 0;
            }
        }
        public FacebookInfo GetFacebookInfo()
        {
            return new FacebookInfo()
            {
                AccessToken = this.localSettings.Values[FacebookAccessTokenKey] as string,
                ProfileId = this.localSettings.Values[FacebookProfileIdKey] as string,
            };
        }
        #endregion
    }
}