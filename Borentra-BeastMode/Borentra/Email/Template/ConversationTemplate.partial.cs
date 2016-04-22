namespace Borentra.Email.Template
{
    using Borentra.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Conversation Template Partial
    /// </summary>
    public partial class ConversationTemplate
    {
        #region Properties
        /// <summary>
        /// Comments
        /// </summary>
        public Comment Latest
        {
            get;
            set;
        }
        #endregion
    }
}