namespace Borentra.API.Internal
{
    using Borentra.API.Models;
    using Borentra.Core;
    using Borentra.DataAccessLayer;
    using Borentra.Security;
    using System;

    public class Authenticator
    {
        #region Properties
        public Device Device
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public bool IsNotValid(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            token = token.TrimIfNotNull();

            if (112 != token.Length)
            {
                return false;
            }

            return this.IsNotValid(token.ToToken());
        }
        public bool IsNotValid(IToken token)
        {
            if (null == token)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(token.AccessToken))
            {
                return false;
            }
            if (112 != token.AccessToken.Length)
            {
                return false;
            }

            return this.IsNotValid(token.ToSecuredToken());
        }

        public bool IsNotValid(ISecuredToken token)
        {
            var isValid = false;
            if (null != token && Guid.Empty != token.Id && !string.IsNullOrWhiteSpace(token.Key))
            {
                // Adding Caching
                var core = new DeviceCore();
                var device = core.Get(token.Id);

                if (null != device && device.FacebookIsValidated && device.KeyExpiresOn > DateTime.UtcNow)
                {
                    //Validate IP in Database with IP of Device

                    isValid = Key.IsValidKey(token.Key, device.Amplitude, device.VerticalOffset, device.AngularFrequency, device.PhaseShift);
                    
                    if (isValid)
                    {
                        this.Device = core.LastValidatedOn(device);
                    }
                }
            }

            return !isValid;
        }
        #endregion
    }
}