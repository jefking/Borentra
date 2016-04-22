namespace Borentra.DataAccessLayer.Admin
{
    using Borentra.Models;

    /// <summary>
    /// Profile Report
    /// </summary>
    public class ProfileReport : Profile
    {
        #region Properties
        public int FriendCount
        {
            get;
            set;
        }

        public int MembersNearbyCount
        {
            get;
            set;
        }

        public int ItemCount
        {
            get;
            set;
        }

        public int ItemRequestCount
        {
            get;
            set;
        }

        public string LandingTheme
        {
            get;
            set;
        }
        #endregion
    }
}