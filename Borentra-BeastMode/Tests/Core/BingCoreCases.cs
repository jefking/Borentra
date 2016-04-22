namespace Tests.Core
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Borentra.Core;

    [TestClass]
    public class BingCoreCases
    {
        [TestMethod]
        public void Constructor()
        {
            new BingCore();
        }
        [TestMethod]
        public void RootUri()
        {
            Assert.AreEqual<string>("https://api.datamarket.azure.com/Bing/Search", BingCore.RootUri);
        }
        [TestMethod]
        public void AccountKey()
        {
            Assert.AreEqual<string>("jiliIP3ZDUIaCCKrbh58qOErKTAcL0k9untZsDG52B0=", BingCore.AccountKey);
        }
    }
}
