namespace Tests.Security
{
    using Borentra;
    using Borentra.Security;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class TokenCreatorCases
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateIdentifierEmpty()
        {
            TokenCreator.Create(Guid.Empty, "asdas");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateKeyEmpty()
        {
            TokenCreator.Create(Guid.NewGuid(), "  ");
        }

        [TestMethod]
        public void Create()
        {
            var identifier = Guid.NewGuid();
            var key = Guid.NewGuid().ToString();
            var data = string.Format("{0}{1}", identifier, key).ToBase64();

            Assert.AreEqual<string>(data, TokenCreator.Create(identifier, key));
        }

        [TestMethod]
        public void CreateWithWhiteSpace()
        {
            var identifier = Guid.NewGuid();
            var key = Guid.NewGuid().ToString();
            var data = string.Format("{0}{1}", identifier, key).ToBase64();

            Assert.AreEqual<string>(data, TokenCreator.Create(identifier, string.Format("  {0}  ", key)));
        }
    }
}