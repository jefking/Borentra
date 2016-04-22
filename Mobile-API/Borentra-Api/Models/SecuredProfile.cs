namespace Borentra.API.Models
{
    using Borentra.Models;

    public class SecuredProfile : Profile, IToken
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