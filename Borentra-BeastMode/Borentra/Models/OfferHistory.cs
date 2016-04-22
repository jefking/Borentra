namespace Borentra.Models
{
    using Borentra.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class OfferHistory : IIdentifier
    {
        #region Properties
        public Guid Identifier
        {
            get;
            set;
        }

        public Guid ItemIdentifier
        {
            get;
            set;
        }

        public string ItemKey
        {
            get;
            set;
        }

        public string ItemTitle
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
                return ImageCore.Thumbnail(PrimaryImagePathFormat);
            }
        }

        public Guid OwnerIdentifier
        {
            get;
            set;
        }

        public string OwnerDisplayName
        {
            get;
            set;
        }

        public string OwnerKey
        {
            get;
            set;
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

        public string RequesterDisplayName
        {
            get;
            set;
        }

        public string RequesterKey
        {
            get;
            set;
        }

        public long RequesterFacebookId
        {
            get;
            set;
        }

        public Uri RequesterPicture
        {
            get
            {
                return FacebookCore.Picture(this.RequesterFacebookId);
            }
        }

        public Guid RequesterUserIdentifier
        {
            get;
            set;
        }
        public DateTime On
        {
            get;
            set;
        }
        public OfferType Type
        {
            get;
            set;
        }
        public byte Status
        {
            get;
            set;
        }
        public string StatusDisplay
        {
            get
            {
                switch(Type)
                {
                    case OfferType.Free:
                        return ((FreeStatus)this.Status).ToString();
                    case OfferType.Rent:
                        return ((RentalStatus)this.Status).ToString();
                    case OfferType.Trade:
                        return string.Empty;
                    case OfferType.Share:
                        return ((BorrowStatus)this.Status).ToString();
                    default:
                        throw new InvalidOperationException();
                }
            }
        }
        #endregion
    }
}