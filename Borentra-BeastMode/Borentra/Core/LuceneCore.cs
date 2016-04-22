namespace Borentra.Core
{
    using Borentra;
    using Borentra.DataAccessLayer;
    using Borentra.DataAccessLayer.Admin;
    using Borentra.DataStore;
    using Borentra.GeoSpatial;
    using Borentra.Models;
    using Borentra.Search;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Index;
    using Lucene.Net.QueryParsers;
    using Lucene.Net.Search;
    using Lucene.Net.Store;
    using Lucene.Net.Store.Azure;
    using Microsoft.WindowsAzure.Storage;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Lucene.NET Search Component
    /// </summary>
    public class LuceneCore
    {
        #region Members
        /// <summary>
        /// Lucene Version
        /// </summary>
        private readonly Lucene.Net.Util.Version version = Lucene.Net.Util.Version.LUCENE_30;

        /// <summary>
        /// Cloud Storage Account
        /// </summary>
        private readonly CloudStorageAccount account = AzureStorage.Get(StorageAccounts.Default);

        /// <summary>
        /// Max Results
        /// </summary>
        private const int MaxResults = 100;

        /// <summary>
        /// Cache Directory
        /// </summary>
        private readonly RAMDirectory cacheDirectory = new RAMDirectory();

        /// <summary>
        /// Search Container
        /// </summary>
        private const string contentContainer = "search";

        /// <summary>
        /// Profile Core
        /// </summary>
        private readonly ProfileCore profileCore = new ProfileCore();

        /// <summary>
        /// Search Fields
        /// </summary>
        private static readonly string[] searchFields = new string[] { SearchDocument.ContentKey, SearchDocument.MemberNameKey, SearchDocument.KeyKey, SearchDocument.LocationKey };
        #endregion

        #region Methods
        #region Search
        public IEnumerable<SearchResult> Search(string term, Guid? userId = null, int take = MaxResults, Reference type = Reference.None)
        {
            term = term.TrimIfNotNull();

            var results = new List<SearchResult>();
            using (var azureDirectory = new AzureDirectory(account, contentContainer, cacheDirectory))
            {
                using (var searcher = new IndexSearcher(azureDirectory))
                {
                    var query = this.DefineQuery(term, userId, type);

                    var hits = searcher.Search(query, null, take, Sort.RELEVANCE).ScoreDocs;
                    var docResults = hits.Select(hit => searcher.Doc(hit.Doc).ToSearchDocument());

                    foreach (var result in from d in docResults
                                           where d.IsVisible(userId)
                                           select d.ToSearchResult())
                    {
                        results.Add(result);
                    }
                }
            }

            return results;
        }
        private Query DefineQuery(string term, Guid? userId, Reference type)
        {
            BooleanQuery masterQuery = null;
            if (!string.IsNullOrWhiteSpace(term))
            {
                using (var analyzer = new StandardAnalyzer(version))
                {
                    var parser = new MultiFieldQueryParser(version, searchFields, analyzer);
                    var termQuery = parser.ParseQuery(term);
                    if (null != termQuery)
                    {
                        masterQuery = masterQuery ?? new BooleanQuery();
                        masterQuery.Add(termQuery, Occur.SHOULD);
                    }
                }
            }

            if (userId.HasValue && Guid.Empty != userId.Value)
            {
                if (masterQuery == null)
                {
                    masterQuery = this.UserQuery(userId.Value);
                }
                else
                {
                    masterQuery.Add(new BooleanClause(this.UserQuery(userId.Value), Occur.MUST));
                }
            }

            if (Reference.None != type)
            {
                if (masterQuery == null)
                {
                    masterQuery = this.TypeQuery(type);
                }
                else
                {
                    masterQuery.Add(new BooleanClause(this.TypeQuery(type), Occur.MUST));
                }
            }

            return (Query)masterQuery ?? new WildcardQuery(new Term(searchFields[0], "*"));
        }
        private BooleanQuery TypeQuery(Reference type)
        {
            if (type == Reference.None)
            {
                throw new ArgumentException("type");
            }

            var typeQuery = NumericRangeQuery.NewIntRange(SearchDocument.TypeKey, (int)type, (int)type, true, true);
            var boolQuery = new BooleanQuery();
            boolQuery.Add(new BooleanClause(typeQuery, Occur.MUST));
            return boolQuery;
        }
        private BooleanQuery UserQuery(Guid userId)
        {
            if (Guid.Empty == userId)
            {
                throw new ArgumentException("userId");
            }

            var profile = profileCore.SearchSingle(userId, null, userId);

            var radius = profile.SearchRadius > 1000000 ? 1000000 : profile.SearchRadius;
            var coordinates = profile.GetCoordinate();

            var ne = coordinates.MoveTo(radius, 45);
            var sw = coordinates.MoveTo(radius, 225);

            var latQuery = NumericRangeQuery.NewDoubleRange(SearchDocument.LatitudeKey, sw.Latitude, ne.Latitude, true, true);
            var longQuery = NumericRangeQuery.NewDoubleRange(SearchDocument.LongitudeKey, sw.Longitude, ne.Longitude, true, true);

            var boolQuery = new BooleanQuery();
            boolQuery.Add(new BooleanClause(latQuery, Occur.MUST));
            boolQuery.Add(new BooleanClause(longQuery, Occur.MUST));
            return boolQuery;
        }
        #endregion

        #region Indexing
        #region Content
        public void IndexContent()
        {
            var contents = new AdminGetSearchableContent().CallObjects<SearchDocument>();

            using (var azureDirectory = new AzureDirectory(account, contentContainer, cacheDirectory))
            {
                using (var analyzer = new StandardAnalyzer(version))
                {
                    using (var indexWriter = new IndexWriter(azureDirectory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
                    {
                        foreach (var content in contents)
                        {
                            try
                            {
                                content.UserIdentifiers = this.UserPermissions(content.Identifier, content.Type);

                                var doc = content.ToDocument();

                                 indexWriter.AddDocument(doc);
                            }
                            catch (Exception ex)
                            {
                                Trace.Write(ex);
                            }
                        }

                        indexWriter.Optimize();
                    }
                }
            }
        }

        /// <summary>
        /// User Permissions
        /// </summary>
        /// <param name="identifier">Identifier</param>
        /// <param name="type">Type</param>
        /// <returns>User Identifiers</returns>
        private Guid[] UserPermissions(Guid identifier, Reference type)
        {
            IStoredProc proc = null;
            switch(type)
            {
                case Reference.Item:
                    proc = new AdminGetItemUsers()
                    {
                        Identifier = identifier,
                    };
                    break;
                case Reference.User:
                    proc = new AdminGetProfileUsers()
                    {
                        UserIdentifier = identifier,
                    };
                    break;
                case Reference.ItemRequest:
                    proc = new AdminGetItemRequestUsers()
                    {
                        Identifier = identifier,
                    };
                    break;
            }

            var permissions = proc.CallObjects<SearchPermissions>();
            return permissions.Select(p => p.UserIdentifier).ToArray();
        }
        #endregion
        #endregion
        #endregion
    }
}