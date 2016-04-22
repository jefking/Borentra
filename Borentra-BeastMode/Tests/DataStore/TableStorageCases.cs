namespace Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Borentra.DataStore;
    using Borentra.DataAccessLayer.Table;

    [TestClass]
    public class TableStorageCases
    {
        [TestMethod]
        public void IsAzureStorage()
        {
            Assert.IsNotNull(new TableStorage("test") as AzureStorage);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorNullTableName()
        {
            new TableStorage(null);
        }
        [TestMethod]
        public void CreateTable()
        {
            var tableName = 'a' + Guid.NewGuid().ToString().Replace('-', 'a');
            var table = new TableStorage(tableName);
            table.Create().Wait();

            var account = AzureStorage.Get();

            var tableClient = account.CreateCloudTableClient();
            var tblRef = tableClient.GetTableReference(tableName);
            var result = tblRef.CreateIfNotExists();

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void RoundTrip()
        {
            var item = new BingQueryEntry()
            {
                PartitionKey = Guid.NewGuid().ToString(),
                RowKey = Guid.NewGuid().ToString(),
                ThumbnailUrl = Guid.NewGuid().ToString(),
                Url = Guid.NewGuid().ToString(),
            };

            var table = new TableStorage("testrt");
            
            table.InsertOrReplace(item).Wait();

            var results = table.QueryByPartition<BingQueryEntry>(item.PartitionKey);
            var result = results.FirstOrDefault();
            Assert.IsNotNull(results);

            Assert.AreEqual<string>(item.PartitionKey, result.PartitionKey);
            Assert.AreEqual<string>(item.RowKey, result.RowKey);
            Assert.AreEqual<string>(item.Url, result.Url);
            Assert.AreEqual<string>(item.ThumbnailUrl, result.ThumbnailUrl);
        }
    }
}