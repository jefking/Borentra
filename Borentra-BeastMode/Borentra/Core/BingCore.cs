namespace Borentra.Core
{
    using Borentra.DataAccessLayer.Table;
    using Borentra.DataStore;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// Bing Core
    /// </summary>
    public class BingCore
    {
        #region Members
        /// <summary>
        /// Root Uri
        /// </summary>
        public const string RootUri = "https://api.datamarket.azure.com/Bing/Search";

        /// <summary>
        /// Account Key
        /// </summary>
        public const string AccountKey = "jiliIP3ZDUIaCCKrbh58qOErKTAcL0k9untZsDG52B0=";

        /// <summary>
        /// Storage
        /// </summary>
        private readonly TableStorage storage = new TableStorage("bingquery");
        #endregion

        #region Methods
        /// <summary>
        /// Image Search
        /// </summary>
        /// <param name="query">Query String</param>
        /// <param name="longitude">Longitude</param>
        /// <param name="latitude">Latitude</param>
        /// <returns>Results</returns>
        public IEnumerable<ImageResult> Search(string query, double? longitude = null, double? latitude = null)
        {
            var results = this.QueryLocal(query);
            if (null == results || 0 == results.Count())
            {
                results = results ?? new List<ImageResult>();
                var bingContainer = new Bing.BingSearchContainer(new Uri(RootUri))
                {
                    Credentials = new NetworkCredential(AccountKey, AccountKey),
                };

                var imageQuery = bingContainer.Image(query, null, null, "Strict", latitude, longitude, "Size:Large+Style:Photo");

                var entries = new List<BingQueryEntry>();
                var images = imageQuery.Execute();
                foreach (var image in images)
                {
                    if (null != image)
                    {
                        var result = new ImageResult(image);
                        results.Add(result);

                        var entry = new BingQueryEntry()
                        {
                            PartitionKey = query.ToLowerInvariant(),
                            RowKey = Guid.NewGuid().ToString(),
                            Url = result.Url,
                            ThumbnailUrl = result.ThumbnailUrl,
                        };

                        entries.Add(entry);
                    }
                }

                this.SaveLocal(entries);
            }

            return results;
        }
        
        /// <summary>
        /// Save Local
        /// </summary>
        /// <param name="entries"></param>
        private void SaveLocal(IEnumerable<BingQueryEntry> entries)
        {
            if (null != entries && 0 < entries.Count())
            {
                try
                {
                    this.storage.Insert(entries);
                }
                catch
                {
                    // Logging
                }
            }
        }

        /// <summary>
        /// Query Locally for Results
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private IList<ImageResult> QueryLocal(string query)
        {
            try
            {
                query = query.ToLowerInvariant();
                var items = this.storage.QueryByPartition<BingQueryEntry>(query);
                if (null != items)
                {
                    var results = new List<ImageResult>(items.Count());
                    foreach (var item in items)
                    {
                        var img = new ImageResult()
                        {
                            ThumbnailUrl = item.ThumbnailUrl,
                            Url = item.Url,
                        };

                        results.Add(img);
                    }

                    return results.OrderBy(x => Guid.NewGuid()).ToList();
                }
            }
            catch
            {
            }

            return null;
        }

        public async void BustCache()
        {
            await this.storage.Delete();
        }
        #endregion
    }
}