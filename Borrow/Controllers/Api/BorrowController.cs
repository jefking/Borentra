namespace Borentra.Controllers.Api
{
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Web.Http;

    /// <summary>
    /// Handles the HTTP layer for borrowing.
    /// </summary>
    [Authorize]
    public class BorrowController : ApiController
    {
        #region Members
        /// <summary>
        /// Borrowing Core Functionality
        /// </summary>
        private readonly BorrowCore borrow = new BorrowCore();
        #endregion

        #region Methods
        [HttpPost]
        [System.Web.Http.ActionName("Request")]
        public ItemShare RequestBorrow(BorrowRequest request)
        {
            if (null == request)
            {
                throw new ArgumentNullException("request");
            }

            var userId = User.Identifier();

            return this.borrow.Request(request, userId);
        }

        [HttpPost]
        public void Delete(DeleteAction delete)
        {
            if (null == delete)
            {
                throw new ArgumentNullException("delete");
            }

            if (Guid.Empty == delete.Identifier)
            {
                throw new ArgumentException("Identifier");
            }

            var userId = User.Identifier();

            this.borrow.Delete(delete, userId);
        }

        [HttpPost]
        public ItemShare Accept(BorrowAction accept)
        {
            if (null == accept)
            {
                throw new ArgumentNullException("accept");
            }

            if (Guid.Empty == accept.Identifier)
            {
                throw new ArgumentException("Identifier");
            }

            var userId = User.Identifier();

            return this.borrow.Accept(accept, userId);
        }

        [HttpPost]
        public ItemShare Reject(BorrowAction reject)
        {
            if (null == reject)
            {
                throw new ArgumentNullException("reject");
            }

            if (Guid.Empty == reject.Identifier)
            {
                throw new ArgumentException("Identifier");
            }

            var userId = User.Identifier();

            return this.borrow.Reject(reject, userId);
        }

        [HttpPost]
        public ItemShare Return(BorrowReturned returned)
        {
            if (null == returned)
            {
                throw new ArgumentNullException("returned");
            }

            if (Guid.Empty == returned.Identifier)
            {
                throw new ArgumentException("Identifier");
            }

            var userId = User.Identifier();

            return this.borrow.Return(returned, userId);
        }
        #endregion
    }
}