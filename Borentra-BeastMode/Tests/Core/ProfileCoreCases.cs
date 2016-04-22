namespace Tests.Core
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Borentra.Core;

    [TestClass]
    public class ProfileCoreCases
    {
        [TestMethod]
        public void Constructor()
        {
            new ProfileCore();
        }

        [TestMethod]
        public void AdministratorIdentifier()
        {
            Assert.AreEqual<Guid>(Guid.Parse("BA8B47BD-63CE-40B7-B68E-03D2320AA279"), ProfileCore.AdministratorIdentifier);
        }

        [TestMethod]
        public void BaseUrlNull()
        {
            var data = "/profile/";
            Assert.AreEqual<string>(data, ProfileCore.BaseUrl(null));
        }

        [TestMethod]
        public void BaseUrl()
        {
            var key = Guid.NewGuid().ToString();
            var data = "/profile/" + key;
            Assert.AreEqual<string>(data, ProfileCore.BaseUrl(key));
        }

        [TestMethod]
        public void SearchUrlNull()
        {
            var data = "/search/member?s=&c=organic";
            Assert.AreEqual<string>(data, ProfileCore.SearchUrl(null));
        }

        [TestMethod]
        public void SearchUrl()
        {
            var term = Guid.NewGuid().ToString();
            var data = "/search/member?s=" + term + "&c=organic";
            Assert.AreEqual<string>(data, ProfileCore.SearchUrl(term));
        }
    }
}
