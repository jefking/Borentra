namespace Borentra.DataAccessLayer.Facebook
{
    using Borentra.Models;

    public class MeInfo : IFacebookEntity
    {
        #region Properties
        public long FacebookId
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
        public string FacebookAccessToken
        {
            get;
            set;
        }

        public System.DateTime FacebookTokenExpiration
        {
            get;
            set;
        }
        #endregion
    }
}