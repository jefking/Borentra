namespace Borentra.Search
{
    using Borentra.DataAccessLayer.Admin;
    using Borentra.Models;
    using Lucene.Net.Documents;
    using Lucene.Net.QueryParsers;
    using Lucene.Net.Search;
    using System;
    using System.Linq;

    public static class ExtensionMethods
    {
        #region Document
        public static SearchDocument ToSearchDocument(this Document doc)
        {
            var data = new SearchDocument()
            {
                Identifier = Guid.Parse(doc.Get(SearchDocument.IdentifierKey)),
                Title = doc.Get(SearchDocument.TitleKey),
                Description = doc.Get(SearchDocument.DescriptionKey),
                ImageData = doc.Get(SearchDocument.ImageDataKey),
                Key = doc.Get(SearchDocument.KeyKey),
                Location = doc.Get(SearchDocument.LocationKey),
                MemberName = doc.Get(SearchDocument.MemberNameKey),
            };

            var type = doc.Get(SearchDocument.TypeKey);
            if (!string.IsNullOrWhiteSpace(type))
            {
                data.Type = (Reference)int.Parse(type);
            }

            var createdOn = doc.Get(SearchDocument.CreatedOnKey);
            if (!string.IsNullOrWhiteSpace(createdOn))
            {
                data.CreatedOn = createdOn.DateTimeExact();
            }

            data.SetPermissions(doc.Get(SearchDocument.PermissionsKey));

            return data;
        }
        #endregion

        #region SearchDocument
        public static bool IsVisible(this SearchDocument doc, Guid? userIdenfitier)
        {
            return null != doc && (null == doc.UserIdentifiers || doc.UserIdentifiers.Any(id => id == userIdenfitier));
        }
        public static SearchResult ToSearchResult(this SearchDocument doc)
        {
            var result = doc.Map<SearchResult>();

            result.SetThumbnail(doc.ImageData);

            return result;
        }
        public static Document ToDocument(this SearchDocument content)
        {
            var doc = new Document();

            doc.Add(new Field(SearchDocument.IdentifierKey, content.Identifier.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field(SearchDocument.UserIdentifierKey, content.UserIdentifier.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field(SearchDocument.MemberNameKey, content.MemberName, Field.Store.YES, Field.Index.ANALYZED));

            var typeField = new NumericField(SearchDocument.TypeKey, Field.Store.YES, true);
            typeField.SetIntValue((int)content.Type);
            doc.Add(typeField);
            var latField = new NumericField(SearchDocument.LatitudeKey, Field.Store.YES, true);
            latField.SetDoubleValue(content.Latitude);
            doc.Add(latField);
            var longField = new NumericField(SearchDocument.LongitudeKey, Field.Store.YES, true);
            longField.SetDoubleValue(content.Longitude);
            doc.Add(longField);

            doc.Add(new Field(SearchDocument.CreatedOnKey, content.CreatedOn.ToStringExact(), Field.Store.YES, Field.Index.NO));

            var permissions = content.Permissions();
            if (!string.IsNullOrWhiteSpace(permissions))
            {
                doc.Add(new Field(SearchDocument.PermissionsKey, permissions, Field.Store.YES, Field.Index.NO));
            }

            if (!string.IsNullOrWhiteSpace(content.ImageData))
            {
                doc.Add(new Field(SearchDocument.ImageDataKey, content.ImageData, Field.Store.YES, Field.Index.NO));
            }

            if (!string.IsNullOrWhiteSpace(content.Title))
            {
                doc.Add(new Field(SearchDocument.TitleKey, content.Title, Field.Store.YES, Field.Index.NO));
            }

            if (!string.IsNullOrWhiteSpace(content.Description))
            {
                doc.Add(new Field(SearchDocument.DescriptionKey, content.Description, Field.Store.YES, Field.Index.NO));
            }

            if (!string.IsNullOrWhiteSpace(content.Key))
            {
                doc.Add(new Field(SearchDocument.KeyKey, content.Key, Field.Store.YES, Field.Index.NOT_ANALYZED));
            }

            if (!string.IsNullOrWhiteSpace(content.Content))
            {
                doc.Add(new Field(SearchDocument.ContentKey, content.Content, Field.Store.YES, Field.Index.ANALYZED));
            }

            if (!string.IsNullOrWhiteSpace(content.Location))
            {
                doc.Add(new Field(SearchDocument.LocationKey, content.Location, Field.Store.YES, Field.Index.ANALYZED));
            }
            
            return doc;
        }
        #endregion

        #region QueryParser
        public static Query ParseQuery(this QueryParser parser, string searchQuery)
        {
            Query query = null;
            try
            {
                query = parser.Parse(searchQuery);
            }
            catch (ParseException)
            {
                query = parser.Parse(QueryParser.Escape(searchQuery));
            }

            return query;
        }
        #endregion
    }
}