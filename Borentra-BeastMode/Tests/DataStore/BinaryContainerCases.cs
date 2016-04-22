namespace Borentra.Test.DataStore
{
    using Borentra.DataStore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class BinaryContainerCases
    {
        [TestMethod]
        public void Constructor()
        {
            new BinaryContainer("asdasd");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorNullContainerName()
        {
            new BinaryContainer(null);
        }
        [TestMethod]
        public void CreateContainer()
        {
            var name = 'z' + Guid.NewGuid().ToString().Replace('-', 'a');
            var blobs = new BinaryContainer(name);
            blobs.Create().Wait();

            var account = AzureStorage.Get();

            var client = account.CreateCloudBlobClient();
            var reference = client.GetContainerReference(name);
            var result = reference.CreateIfNotExists();

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void RoundTrip()
        {
            var data = new byte[64];
            var random = new Random();
            random.NextBytes(data);
            var objId = "a" + Guid.NewGuid().ToString();
            var container = new BinaryContainer("test");
            container.Create().Wait();

            container.Save(objId, data);

            var result = container.Get(objId);
            Assert.IsNotNull(result);
            Assert.AreEqual<int>(data.Length, result.Length);
            for (var i = 0; i < data.Length; i++)
            {
                Assert.AreEqual<byte>(data[i], result[i]);
            }
        }
    }
}