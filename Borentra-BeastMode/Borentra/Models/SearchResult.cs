namespace Borentra.Models
{
    using Borentra.Core;
    using System;
    using System.Collections.Generic;

    public class SearchResult : IKey, IContent, IReferenceType
    {
        #region Properties
        public string Title
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }
        public string Key
        {
            get;
            set;
        }
        public Reference Type
        {
            get;
            set;
        }
        public string Thumbnail
        {
            get;
            set;
        }
        public string MemberName
        {
            get;
            set;
        }
        public string Location
        {
            get;
            set;
        }
        public DateTime CreatedOn
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public void SetThumbnail(string data)
        {
            if (!string.IsNullOrWhiteSpace(data))
            {
                switch (this.Type)
                {
                    case Reference.User:
                    case Reference.Company:
                        this.Thumbnail = FacebookCore.Picture(long.Parse(data)).ToString();
                        break;
                    case Reference.ItemRequest:
                    case Reference.Item:
                        this.Thumbnail = ImageCore.ThumbnailCdn(data);
                        break;
                }
            }
        }
        #endregion
    }
}