namespace Borentra.API.Models
{
    using Borentra.Core;
    using Borentra.Models;
    using System;

    public class Offer
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
        public static Offer Map(Item item)
        {
            var offer = item.Map<Offer>();
            offer.Thumbnail = ImageCore.Cdn(item.PrimaryImageThumbnail);
            offer.LargeImage = ImageCore.Cdn(item.PrimaryImageLarge);
            offer.OwnerPicture = item.OwnerPicture;
            return offer;
        }
        #endregion
    }
}