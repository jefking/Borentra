namespace Borentra.Models
{
    using Borentra.Core;
    using Borentra.Web;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Item RSS
    /// </summary>
    public class ItemRss : IRss, IReferenceType
    {
        #region Properties
        public string Title
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public string Tags
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Link
        {
            get
            {
                return this.Type == Reference.ItemRequest ? ItemRequestCore.BaseUrl(this.Key) : ItemCore.BaseUrl(this.Key);
            }
        }

        public DateTime ModifiedOn
        {
            get;
            set;
        }

        public DateTime PublishedOn
        {
            get
            {
                return this.ModifiedOn;
            }
        }

        public string PrimaryImagePathFormat
        {
            get;
            set;
        }

        public string Image
        {
            get
            {
                return ImageCore.LargeCdn(this.PrimaryImagePathFormat);
            }
        }

        public IEnumerable<string> Categories
        {
            get;
            set;
        }

        public Reference Type
        {
            get;
            set;
        }
        #endregion
    }
}