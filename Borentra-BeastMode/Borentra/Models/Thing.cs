namespace Borentra.Models
{
    using Borentra.Core;
    using System;
    using System.Collections.Generic;

    public class Thing : IWebEntity, IUserIdentifier, IDescriptive
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

        public virtual bool IsPublic
        {
            get
            {
                return false;
            }
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

        public bool Delete
        {
            get;
            set;
        }

        public bool IsMine
        {
            get;
            set;
        }

        public bool IsFriend
        {
            get;
            set;
        }

        public bool IsNew
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

        public string OwnerFirstName
        {
            get
            {
                return this.OwnerName.FirstPart();
            }
        }

        public long OwnerFacebookId
        {
            get;
            set;
        }

        public Uri OwnerPicture
        {
            get
            {
                return FacebookCore.Picture(this.OwnerFacebookId);
            }
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

        public IEnumerable<string> Categories
        {
            get;
            set;
        }

        public string PrimaryImagePathFormat
        {
            get;
            set;
        }

        public string PrimaryImageThumbnail
        {
            get
            {
                return ImageCore.Thumbnail(this.PrimaryImagePathFormat);
            }
        }

        public string PrimaryImageLarge
        {
            get
            {
                return ImageCore.Large(this.PrimaryImagePathFormat);
            }
        }

        public string Tags
        {
            get;
            set;
        }
        #endregion
    }
}