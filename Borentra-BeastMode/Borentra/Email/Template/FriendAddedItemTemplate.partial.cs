namespace Borentra.Email.Template
{
    using Borentra.DataAccessLayer.Facebook;
    using Borentra.Models;

    /// <summary>
    /// Friend Signed Up Template Partial
    /// </summary>
    public partial class FriendAddedItemTemplate
    {
        #region Properties

        public Profile Me
        {
            get;
            set;
        }

        public Profile Friend
        {
            get;
            set;
        }

        public Item Item
        {
            get;
            set;
        }

        #endregion
    }
}