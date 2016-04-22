namespace Borentra.API.Models
{
    using Borentra.Models;

    public class SecuredItem : Item, IToken
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