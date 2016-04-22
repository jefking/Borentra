namespace Borentra.Controllers.Api
{
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Web.Http;

    /// <summary>
    /// Free API Controller
    /// </summary>
    public class FreeController : ApiController
    {
        #region Members
        /// <summary>
        /// Free Core
        /// </summary>
        private readonly FreeCore freeCore = new FreeCore();
        #endregion

        #region Methods
        // GET:/Api/Free/Cancel
        // Identifier = ItemFree.Identifier
        [HttpGet]
        public void Cancel(Guid identifier)
        {
            if (Guid.Empty == identifier)
            {
                throw new ArgumentException("identifier");
            }

            var userId = User.Identifier();

            freeCore.Cancel(userId, identifier);
        }

        // GET:/Api/Free/Decline
        // Identifier = ItemFree.Identifier
        [HttpGet]
        public ItemFree Decline(Guid identifier, string comment)
        {
            if (Guid.Empty == identifier)
            {
                throw new ArgumentException("identifier");
            }

            var userId = User.Identifier();

            return freeCore.Decline(userId, identifier, comment);
        }

        // GET:/Api/Free/Accept
        // Identifier = ItemFree.Identifier
        [HttpGet]
        public ItemFree Accept(Guid identifier, string comment)
        {
            if (Guid.Empty == identifier)
            {
                throw new ArgumentException("identifier");
            }

            var userId = User.Identifier();

            return freeCore.Accept(userId, identifier, comment);
        }

        // GET:/Api/Free/Request
        // Identifier = ItemFree.ItemIdentifier
        [HttpGet]
        public ItemFree Request(Guid itemIdentifier, string comment)
        {
            if (Guid.Empty == itemIdentifier)
            {
                throw new ArgumentException("identifier");
            }

            var userId = User.Identifier();

            return freeCore.Request(userId, itemIdentifier, comment);
        }
        #endregion
    }
}