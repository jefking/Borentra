namespace Borentra.Models
{
    using Borentra.Core;
    using System;

    public class SocialFavorite : Social
    {
        #region Properties
        public long UserFacebookId
        {
            get;
            set;
        }

        public string UserDisplayName
        {
            get;
            set;
        }

        public string UserKey
        {
            get;
            set;
        }
        
        public Uri Picture
        {
            get
            {
                return FacebookCore.Picture(this.UserFacebookId);
            }
        }
        #endregion
    }
}