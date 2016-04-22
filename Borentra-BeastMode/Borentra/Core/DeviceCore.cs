namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using System;

    public class DeviceCore
    {
        #region Methods
        public Device Save(Device device)
        {
            if (null == device)
            {
                throw new ArgumentNullException("device");
            }

            var sproc = new DeviceSaveDevice()
            {
                Identifier = device.Identifier.ToNullable(),
                Amplitude = device.Amplitude,
                AngularFrequency = device.AngularFrequency,
                PhaseShift = device.PhaseShift,
                VerticalOffset = device.VerticalOffset,
                UserIdentifier = device.UserIdentifier.ToNullable(),
                DeviceIdentifier = device.DeviceIdentifier.ToNullable(),
                FacebookAccessToken = device.FacebookAccessToken,
                FacebookId = device.FacebookId,
                FacebookIsValidated = device.FacebookIsValidated,
                FacebookTokenExpiration = device.FacebookTokenExpiration,
                IpAddress = device.IpAddress,
                Os = (byte?)device.OperatingSystem,
                KeyExpiresOn = device.KeyExpiresOn,
            };

            return sproc.CallObject<Device>();
        }

        public Device LogOut(Guid identifier)
        {
            if (Guid.Empty == identifier)
            {
                throw new ArgumentException("identifier");
            }

            var sproc = new DeviceInvalidateDeviceKey()
            {
                Identifier = identifier,
            };

            return sproc.CallObject<Device>();
        }

        public Device LastValidatedOn(Device device)
        {
            if (null == device)
            {
                throw new ArgumentNullException("device");
            }
            if (Guid.Empty == device.Identifier)
            {
                throw new ArgumentException("identifier");
            }
            if (Guid.Empty == device.DeviceIdentifier)
            {
                throw new ArgumentException("device identifier");
            }
            if (Guid.Empty == device.UserIdentifier)
            {
                throw new ArgumentException("user identifier");
            }

            var sproc = new DeviceSaveDevice()
            {
                UserIdentifier = device.UserIdentifier,
                Identifier = device.Identifier,
                DeviceIdentifier = device.DeviceIdentifier,
                LastValidatedOn = DateTime.UtcNow,
            };

            return sproc.CallObject<Device>();
        }

        public Device Get(Guid identifier)
        {
            if (Guid.Empty == identifier)
            {
                throw new ArgumentException("identifier");
            }

            var sproc = new DeviceGetDevice()
            {
                Identifier = identifier,
            };

            return sproc.CallObject<Device>();
        }
        #endregion
    }
}