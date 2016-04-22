namespace Borentra.DataAccessLayer.Admin
{
    using Borentra.Models;
    using System;
    using System.Linq;

    public class SearchDocument : IIdentifier, ILocation, IUserIdentifier, IReferenceType
    {
        #region Members
        public const string ContentKey = "Content";
        public const string TypeKey = "Type";
        public const string PermissionsKey = "Permisions";
        public const string IdentifierKey = "Identifier";
        public const string MemberNameKey = "MemberName";
        public const string ImageDataKey = "ImageData";
        public const string KeyKey = "Key";
        public const string LongitudeKey = "Longitude";
        public const string LatitudeKey = "Latitude";
        public const string LocationKey = "Location";
        public const string TitleKey = "Title";
        public const string DescriptionKey = "Description";
        public const string UserIdentifierKey = "UserIdentifier";
        public const string CreatedOnKey = "CreatedOn";
        #endregion

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
        public string Content
        {
            get;
            set;
        }
        public string Location
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
        public Guid[] UserIdentifiers
        {
            get;
            set;
        }
        public string MemberName
        {
            get;
            set;
        }
        public string ImageData
        {
            get;
            set;
        }
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
        public DateTime CreatedOn
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

        #region Methods
        public string Permissions()
        {
            if (null != this.UserIdentifiers && 0 < this.UserIdentifiers.Count())
            {
                var permissions = this.UserIdentifiers.Serialize();
                if (null != permissions)
                {
                    return permissions.GetHexadecimal();
                }
            }

            return null;
        }

        public void SetPermissions(string serialized)
        {
            if (!string.IsNullOrWhiteSpace(serialized))
            {
                var data = serialized.FromHex();
                if (null != data)
                {
                    this.UserIdentifiers = data.Deserialize<Guid[]>();
                }
            }
        }
        #endregion
    }
}