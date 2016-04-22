namespace Borentra.Test.Core
{
    using Borentra.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class CompanyCoreCases
    {
        [TestMethod]
        public void Constructor()
        {
            new CompanyCore();
        }

        [TestMethod]
        public void BaseUrlNull()
        {
            var data = "/company/";
            Assert.AreEqual<string>(data, CompanyCore.BaseUrl(null));
        }

        [TestMethod]
        public void BaseUrl()
        {
            var key = Guid.NewGuid().ToString();
            var data = "/company/" + key;
            Assert.AreEqual<string>(data, CompanyCore.BaseUrl(key));
        }

        [TestMethod]
        public void SearchUrlNull()
        {
            var data = "/search/company?s=&c=organic";
            Assert.AreEqual<string>(data, CompanyCore.SearchUrl(null));
        }

        [TestMethod]
        public void SearchUrl()
        {
            var term = Guid.NewGuid().ToString();
            var data = "/search/company?s=" + term + "&c=organic";
            Assert.AreEqual<string>(data, CompanyCore.SearchUrl(term));
        }
    }
}