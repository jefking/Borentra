namespace Borentra.DataAccessLayer.Facebook
{
    using Borentra.Models;
    using System;

    /// <summary>
    /// Friend Email
    /// </summary>
    public class FriendEmail : IUserIdentifier
    {
        #region Properties
        /// <summary>
        /// User Display Name
        /// </summary>
        public string UserDisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// User Display Email
        /// </summary>
        public string UserEmail
        {
            get;
            set;
        }

        /// <summary>
        /// User Facebook Id
        /// </summary>
        public long UserFacebookId
        {
            get;
            set;
        }

        public Guid UserIdentifier
        {
            get;
            set;
        }

        /// <summary>
        /// Friend Display Name
        /// </summary>
        public string FriendDisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// Friend Key
        /// </summary>
        public string FriendKey
        {
            get;
            set;
        }

        /// <summary>
        /// Friend Facebook Id
        /// </summary>
        public long FriendFacebookId
        {
            get;
            set;
        }

        public Guid FriendIdentifier
        {
            get;
            set;
        }
        #endregion
    }
}