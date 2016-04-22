namespace Tests.DataStore
{
    using Borentra.DataAccessLayer;
    using Borentra.DataStore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AzureStorageCases
    {
        [TestMethod]
        public void DataConnectionStringKey()
        {
            Assert.AreEqual<string>("DataConnectionString", AzureStorage.DataConnectionStringKey);
        }
        [TestMethod]
        public void SicData2()
        {
            Assert.AreEqual<string>("SicData2", AzureStorage.SicData2);
        }
        [TestMethod]
        public void SicData3()
        {
            Assert.AreEqual<string>("SicData3", AzureStorage.SicData3);
        }
        [TestMethod]
        public void GetDefaultKeyDefault()
        {
            Assert.AreEqual<string>(AzureStorage.DataConnectionStringKey, AzureStorage.Key());
        }
        [TestMethod]
        public void GetDefaultKey()
        {
            Assert.AreEqual<string>(AzureStorage.DataConnectionStringKey, AzureStorage.Key(StorageAccounts.Default));
        }
        [TestMethod]
        public void GetOffsite1Key()
        {
            Assert.AreEqual<string>(AzureStorage.SicData2, AzureStorage.Key(StorageAccounts.Offsite1));
        }
        [TestMethod]
        public void GetAdministrative1Key()
        {
            Assert.AreEqual<string>(AzureStorage.SicData3, AzureStorage.Key(StorageAccounts.Administrative1));
        }
    }
}
