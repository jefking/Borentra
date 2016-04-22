namespace Borentra.Core
{
    using Borentra.Models;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    public class SearchCore
    {
        #region Members
        private readonly HttpCore http = new HttpCore();
        #endregion

        #region Methods
        public async Task<IEnumerable<SearchResult>> Search(Reference type, string term = null, byte limit = 50)
        {
            var url = this.SearchUrl(type);

            if (string.IsNullOrWhiteSpace(term))
            {
                url = string.Format("{0}?limit={1}", url, limit);
            }
            else
            {
                url = string.Format("{0}?term={1}&limit={2}", url, term, limit);
            }

            return await http.Get<List<SearchResult>>(url);
        }
        private string SearchUrl(Reference type)
        {
            return string.Format("{0}/search", this.Url(type));
        }
        private string Url(Reference type)
        {
            var url = "http://api.borentra.com/{0}";
            switch (type)
            {
                case Reference.Offer:
                    url = string.Format(url, "offer");
                    break;
                case Reference.Want:
                    url = string.Format(url, "want");
                    break;
                //case Reference.Member:
                //    url = string.Format(url, "member");
                //    break;
            }

            return url;
        }
        #endregion
    }
}