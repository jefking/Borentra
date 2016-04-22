namespace Borentra.API.Models
{
    using Borentra.Models;

    public class SecuredComment : Comment, IToken
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