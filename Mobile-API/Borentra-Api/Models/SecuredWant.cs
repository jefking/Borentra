namespace Borentra.API.Models
{
    using Borentra.Models;

    public class SecuredWant : ItemRequest, IToken
    {
        #region Properties
        public string AccessToken
        {
            get;
            set;
        }
        public string ImageUrl
        {
            get;
            set;
        }
        #endregion
    }
}