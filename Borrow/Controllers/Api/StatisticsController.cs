namespace Borentra.Controllers.Api
{
    using Borentra.DataStore;
    using Borentra.Models;
    using System.Collections.Generic;
    using System.Web.Http;

    public class StatisticsController : ApiController
    {
        #region Methods
        [HttpGet]
        public IEnumerable<CountryPercentage> MembersByCountry()
        {
            var blob = new JsonContainer();
            return blob.Get<IEnumerable<CountryPercentage>>("globalheatmap");
        }
        #endregion
    }
}