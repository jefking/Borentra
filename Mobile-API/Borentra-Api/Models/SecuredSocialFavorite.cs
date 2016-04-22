namespace Borentra.API.Models
{
    using Borentra.Models;

    public class SecuredSocialFavorite : SocialFavorite, IToken
    {
        #region Properties
        public string AccessToken
        {
            get;
            set;
        }
        #endregion
    }
}