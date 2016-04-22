namespace Borentra.API.Models
{
    using Borentra.Models;
    using System.Collections.Generic;

    public class MobileContacts : IToken
    {
        #region Properties
        public string AccessToken
        {
            get;
            set;
        }
        public IEnumerable<MobileContact> Contacts
        {
            get;
            set;
        }
        #endregion
    }
}