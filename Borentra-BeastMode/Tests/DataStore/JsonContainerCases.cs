namespace Borentra.Test.DataStore
{
    using Borentra.DataAccessLayer.Admin;
    using Borentra.DataStore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TestClass]
    public class JsonContainerCases
    {
        [TestMethod]
        public void RoundTrip()
        {
            var data = new SearchLocationIp()
            {
                Location = Guid.NewGuid().ToString(),
            };

            var objId = "a" + Guid.NewGuid().ToString();
            var container = new JsonContainer("jsontests");
            container.Create().Wait();

            container.Save(objId, data).Wait();

            var result = container.Get<SearchLocationIp>(objId);
            Assert.IsNotNull(result);
            Assert.AreEqual<string>(data.Location, result.Location);
        }
    }
}