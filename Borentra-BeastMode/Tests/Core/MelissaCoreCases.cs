namespace Tests.Core
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Borentra.Core;

    [TestClass]
    public class MelissaCoreCases
    {
        [TestMethod]
        public void Constructor()
        {
            new MelissaCore();
        }
        [TestMethod]
        public void RootUri()
        {
            Assert.AreEqual<string>("https://api.datamarket.azure.com/Data.ashx/MelissaData/IPCheck/v1/", MelissaCore.RootUri);
        }
        [TestMethod]
        public void AccountKey()
        {
            Assert.AreEqual<string>("jiliIP3ZDUIaCCKrbh58qOErKTAcL0k9untZsDG52B0=", MelissaCore.AccountKey);
        }
    }
}
