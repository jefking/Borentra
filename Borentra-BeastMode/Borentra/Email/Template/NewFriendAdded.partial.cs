namespace Borentra.Email.Template
{
    using Borentra.DataAccessLayer.Facebook;

    /// <summary>
    /// New Friend Added
    /// </summary>
    public partial class NewFriendAdded
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