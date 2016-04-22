namespace Tests.Collections
{
    using Borentra.Collections;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class WeightCases
    {
        [TestMethod]
        public void Constructor()
        {
            new Weight<object>();
        }
        [TestMethod]
        public void Count()
        {
            var data = new Weight<object>();
            var count = (byte)new Random().Next(byte.MaxValue);
            data.Count = count;
            Assert.AreEqual<int>(count, data.Count);
        }
        [TestMethod]
        public void Item()
        {
            var data = new Weight<int>();
            var item = new Random().Next();
            data.Item = item;
            Assert.AreEqual<int>(item, data.Item);
        }
    }
}