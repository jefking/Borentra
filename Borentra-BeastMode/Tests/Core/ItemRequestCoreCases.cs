namespace Tests.Core
{
    using Borentra.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class ItemRequestCoreCases
    {
        [TestMethod]
        public void Constructor()
        {
            new ItemRequestCore();
        }

        [TestMethod]
        public void BaseUrlNull()
        {
            var data = "/wanted/";
            Assert.AreEqual<string>(data, ItemRequestCore.BaseUrl(null));
        }

        [TestMethod]
        public void BaseUrl()
        {
            var key = Guid.NewGuid().ToString();
            var data = "/wanted/" + key;
            Assert.AreEqual<string>(data, ItemRequestCore.BaseUrl(key));
        }

        [TestMethod]
        public void SearchUrlNull()
        {
            var data = "/search/wanted?s=&c=organic";
            Assert.AreEqual<string>(data, ItemRequestCore.SearchUrl(null));
        }

        [TestMethod]
        public void SearchUrl()
        {
            var term = Guid.NewGuid().ToString();
            var data = "/search/wanted?s=" + term + "&c=organic";
            Assert.AreEqual<string>(data, ItemRequestCore.SearchUrl(term));
        }
    }
}