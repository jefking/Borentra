namespace Tests.Helpers
{
    using Borentra;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class ExtensionMethodCases
    {
        #region Objects
        private class TestObject
        {
            public string Happy
            {
                get;
                set;
            }
            public DateTime Sad
            {
                get;
                set;
            }
        }
        #endregion

        #region System.Object

        [TestMethod]
        public void ObjectParameter()
        {
            var obj = new object();
            Assert.AreEqual<int>(0, obj.Parameters().Length);
        }

        [TestMethod]
        public void ObjectParameters()
        {
            var obj = new TestObject();
            Assert.AreEqual<int>(2, obj.Parameters().Length);
            foreach (var property in obj.Parameters())
            {
                switch (property)
                {
                    case "Happy":
                    case "Sad":
                        continue;
                    default:
                        Assert.Fail();
                        break;
                }
            }
        }
        #endregion

        #region System.Guid
        [TestMethod]
        public void ToNullableEmpty()
        {
            Assert.IsNull(Guid.Empty.ToNullable());
        }
        [TestMethod]
        public void ToNullableValue()
        {
            var data = Guid.NewGuid();
            Assert.AreEqual<Guid?>(data, data.ToNullable());
        }
        #endregion

        #region System.String
        [TestMethod]
        public void FirstPartNull()
        {
            string data = null;
            Assert.IsNull(data.FirstPart());
        }
        public void FirstPartNoSplitter()
        {
            var data = Guid.NewGuid().ToString();
            Assert.AreEqual<string>(data, data.FirstPart('?'));
        }
        [TestMethod]
        public void FirstPartSplit()
        {
            var first = "jef";
            var data = string.Format("{0} king", first);
            Assert.AreEqual<string>(first, data.FirstPart());
        }
        [TestMethod]
        public void FromBase64String()
        {
            var original = Guid.NewGuid().ToString();
            var base64 = original.ToBase64();

            Assert.AreEqual<string>(original, base64.FromBase64<string>());
        }
        [TestMethod]
        public void TrimIfNotNullNull()
        {
            string data = null;
            Assert.IsNull(data.TrimIfNotNull());
        }
        [TestMethod]
        public void TrimIfNotNullValue()
        {
            var data = Guid.NewGuid().ToString();
            Assert.AreEqual<string>(data, data.TrimIfNotNull());
        }
        [TestMethod]
        public void TrimIfNotNullValueTrim()
        {
            var raw = Guid.NewGuid();
            var data = string.Format("  {0} ", raw);
            Assert.AreEqual<string>(raw.ToString(), data.TrimIfNotNull());
        }
        #endregion
    }
}