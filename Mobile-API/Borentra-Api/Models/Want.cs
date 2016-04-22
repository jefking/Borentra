namespace Borentra.API.Models
{
    using Borentra.Core;
    using Borentra.Models;
    using System;

    public class Want
    {
        #region Properties
        public Guid Identifier
        {
            get;
            set;
        }
        public Guid UserIdentifier
        {
            get;
            set;
        }
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

        public string Description
        {
            get;
            set;
        }

        public string OwnerKey
        {
            get;
            set;
        }

        public string OwnerName
        {
            get;
            set;
        }

        public Uri OwnerPicture
        {
            get;
            set;
        }

        public string Location
        {
            get;
            set;
        }

        public double Latitude
        {
            get;
            set;
        }

        public double Longitude
        {
            get;
            set;
        }

        public DateTime CreatedOn
        {
            get;
            set;
        }
        public string Thumbnail
        {
            get;
            set;
        }

        public string LargeImage
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public static Want Map(ItemRequest itemRequest)
        {
            var want = itemRequest.Map<Want>();
            want.Thumbnail = ImageCore.Cdn(itemRequest.PrimaryImageThumbnail);
            want.LargeImage = ImageCore.Cdn(itemRequest.PrimaryImageLarge);
            want.OwnerPicture = itemRequest.OwnerPicture;
            return want;
        }
        #endregion
    }
}