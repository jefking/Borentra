namespace Borentra.DataAccessLayer
{
    using Borentra.Models;
    using System;

    /// <summary>
    /// Profile Full
    /// </summary>
    public class ProfileFull : Profile, IFacebookAccess
    {
        #region Properties
        /// <summary>
        /// Facebook Access Token
        /// </summary>
        public string FacebookAccessToken
        {
            get;
            set;
        }

        public DateTime FacebookTokenExpiration
        {
            get;
            set;
        }
        #endregion
    }
}