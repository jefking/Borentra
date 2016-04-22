namespace Borentra.Models
{
    using Borentra.Core;
    using System;

    public class ItemImage : IIdentifier
    {
        #region Properties
        public string VirtualPathFormat
        {
            get;
            set;
        }

        public string Thumbnail
        {
            get
            {
                return ImageCore.Thumbnail(this.VirtualPathFormat);
            }
        }

        public string Large
        {
            get
            {
                return ImageCore.Large(this.VirtualPathFormat);
            }
        }

        public string Original
        {
            get
            {
                return ImageCore.Original(this.VirtualPathFormat);
            }
        }

        public Guid Identifier
        {
            get;
            set;
        }
        #endregion
    }
}