namespace Borentra.Models
{
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
        public string CreatedOnShort
        {
            get
            {
                return this.CreatedOn.ToString("MMM yyyy");
            }
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
    }
}