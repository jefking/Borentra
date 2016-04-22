namespace Borentra.Models
{
    using System;

    /// <summary>
    /// Conversation Search
    /// </summary>
    public class ConversationSearch : IIdentifier, IUserIdentifier
    {
        #region Properties
        /// <summary>
        /// User Identifier
        /// </summary>
        public Guid UserIdentifier
        {
            get;
            set;
        }

        /// <summary>
        /// Conversation Identifier
        /// </summary>
        public Guid Identifier
        {
            get;
            set;
        }
        #endregion
    }
}