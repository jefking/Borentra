namespace Borentra.Models
{
    using Borentra.DataAccessLayer;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Member Profile
    /// </summary>
    public class Profile : IWebEntity, IFacebookIdentity
    {
        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Profile()
        {
            this.Activities = new List<Activity>(10);
        }
        #endregion

        #region Properties
        public string IpAddress
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

        public PrivacyLevel PrivacyLevel
        {
            get;
            set;
        }

        public Guid Identifier
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public long FacebookId
        {
            get;
            set;
        }

        public Uri Picture
        {
            get
            {
                return this.Picture();
            }
        }

        public IEnumerable<Activity> Activities
        {
            get;
            set;
        }

        public IEnumerable<Badge> Badges
        {
            get;
            set;
        }

        public IEnumerable<Item> Items
        {
            get;
            set;
        }

        public double Latitude
        {
            get;
            set;
        }

        public int GiveCount
        {
            get;
            set;
        }
        public int FriendsOffersCount
        {
            get;
            set;
        }
        public int Points
        {
            get;
            set;
        }
        public int RecieveCount
        {
            get;
            set;
        }

        public int LendCount
        {
            get;
            set;
        }

        public int BorrowCount
        {
            get;
            set;
        }

        public int TradeCount
        {
            get;
            set;
        }

        public double Longitude
        {
            get;
            set;
        }

        public int SearchRadius
        {
            get;
            set;
        }

        public bool IsNew
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

        public bool IsPublic
        {
            get
            {
                return this.PrivacyLevel == DataAccessLayer.PrivacyLevel.Public;
            }
        }

        public string Email
        {
            get;
            set;
        }
        #endregion
    }
}