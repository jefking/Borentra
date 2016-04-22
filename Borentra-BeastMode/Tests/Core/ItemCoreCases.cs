namespace Tests.Core
{
    using Borentra.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class ItemCoreCases
    {
        [TestMethod]
        public void Constructor()
        {
            new ItemCore();
        }

        [TestMethod]
        public void AdministratorIdentifier()
        {
            Assert.AreEqual<Guid>(Guid.Parse("BA8B47BD-63CE-40B7-B68E-03D2320AA279"), ItemCore.AdministratorIdentifier);
        }

        [TestMethod]
        public void BaseUrlNull()
        {
            var data = "/offer/";
            Assert.AreEqual<string>(data, ItemCore.BaseUrl(null));
        }

        [TestMethod]
        public void BaseUrl()
        {
            var key = Guid.NewGuid().ToString();
            var data = "/offer/" + key;
            Assert.AreEqual<string>(data, ItemCore.BaseUrl(key));
        }

        [TestMethod]
        public void SearchUrlNull()
        {
            var data = "/search/offer?s=&c=organic";
            Assert.AreEqual<string>(data, ItemCore.SearchUrl(null));
        }

        [TestMethod]
        public void SearchUrl()
        {
            var term = Guid.NewGuid().ToString();
            var data = "/search/offer?s=" + term + "&c=organic";
            Assert.AreEqual<string>(data, ItemCore.SearchUrl(term));
        }
    }
}