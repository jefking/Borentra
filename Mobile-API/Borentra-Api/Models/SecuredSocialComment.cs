namespace Borentra.API.Models
{
    using Borentra.Models;

    public class SecuredSocialComment : SocialComment, IToken
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