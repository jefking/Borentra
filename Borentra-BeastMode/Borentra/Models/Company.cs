namespace Borentra.Models
{
    using Borentra.Core;
    using System;

    public class Company : IIdentifier, IKey, ILocation
    {
        #region Properties
        public Guid Identifier
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }
        
        public string PhoneNumber
        {
            get;
            set;
        }

        public string WebsiteUrl
        {
            get;
            set;
        }

        public string LogoPath
        {
            get;
            set;
        }

        public string BannerPath
        {
            get;
            set;
        }

        public string LogoLarge
        {
            get
            {
                return ImageCore.LargeCdn(this.LogoPath);
            }
        }

        public string BannerLarge
        {
            get
            {
                return ImageCore.LargeCdn(this.BannerPath);
            }
        }

        public string LogoThumbnail
        {
            get
            {
                return ImageCore.ThumbnailCdn(this.LogoPath);
            }
        }

        public DateTime CreatedOn
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
        #endregion
    }
}