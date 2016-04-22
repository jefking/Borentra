namespace Tests.Core
{
    using Borentra.Core;
    using Borentra.DataAccessLayer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class DeviceCoreCases
    {
        [TestMethod]
        public void Constructor()
        {
            new DeviceCore();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SaveNull()
        {
            var core = new DeviceCore();
            core.Save(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LogoutEmpty()
        {
            var core = new DeviceCore();
            core.LogOut(Guid.Empty);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LastValidatedOnNull()
        {
            var core = new DeviceCore();
            core.LastValidatedOn(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LastValidatedOnIdentifierEmpty()
        {
            var core = new DeviceCore();
            var device = new Device()
            {
                Identifier = Guid.Empty,
                UserIdentifier = Guid.NewGuid(),
                DeviceIdentifier = Guid.NewGuid(),
            };
            core.LastValidatedOn(device);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LastValidatedOnUserIdentifierEmpty()
        {
            var core = new DeviceCore();
            var device = new Device()
            {
                Identifier = Guid.NewGuid(),
                UserIdentifier = Guid.Empty,
                DeviceIdentifier = Guid.NewGuid(),
            };
            core.LastValidatedOn(device);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LastValidatedOnDeviceIdentifierEmpty()
        {
            var core = new DeviceCore();
            var device = new Device()
            {
                Identifier = Guid.NewGuid(),
                UserIdentifier = Guid.NewGuid(),
                DeviceIdentifier = Guid.Empty,
            };
            core.LastValidatedOn(device);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetIdentifierEmpty()
        {
            var core = new DeviceCore();
            core.Get(Guid.Empty);
        }
    }
}