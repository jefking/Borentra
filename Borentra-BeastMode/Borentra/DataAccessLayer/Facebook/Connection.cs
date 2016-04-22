namespace Borentra.DataAccessLayer.Facebook
{
    using Borentra.Models;
    using System;

    /// <summary>
    /// Connection
    /// </summary>
    public class Connection : IFacebookIdentity
    {
        #region Properties
        /// <summary>
        /// Facebook Identifier
        /// </summary>
        public long FacebookId
        {
            get;
            set;
        }

        public Guid OwnerIdentifier
        {
            get;
            set;
        }
        #endregion
    }
}