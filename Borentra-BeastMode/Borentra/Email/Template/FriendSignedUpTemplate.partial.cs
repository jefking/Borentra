namespace Borentra.Email.Template
{
    using Borentra.DataAccessLayer.Facebook;

    /// <summary>
    /// Friend Signed Up Template Partial
    /// </summary>
    public partial class FriendSignedUpTemplate
    {
        #region Properties
        /// <summary>
        /// Friend Email
        /// </summary>
        public FriendEmail Friend
        {
            get;
            set;
        }
        #endregion
    }
}