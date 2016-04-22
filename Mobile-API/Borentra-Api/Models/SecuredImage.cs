namespace Borentra.API.Models
{
    using System;

    public class SecuredImage : IToken
    {
        #region Properties
        public string AccessToken
        {
            get;
            set;
        }
        public string Url
        {
            get;
            set;
        }
        public Guid ReferenceIdentifier
        {
            get;
            set;
        }
        #endregion
    }
}