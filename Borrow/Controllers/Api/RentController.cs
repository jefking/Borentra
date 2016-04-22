namespace Borentra.Controllers.Api
{
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Web.Http;

    [Authorize(Roles = "staff")]
    public class RentController : ApiController
    {
        #region Members
        /// <summary>
        /// Rent Core
        /// </summary>
        private readonly RentCore rentCore = new RentCore();
        #endregion

        #region Methods
        [HttpPost]
        [System.Web.Http.ActionName("Request")]
        public ItemRenting RequestRent(RentalRequest request)
        {
            if (null == request)
            {
                throw new ArgumentNullException("request");
            }

            var userId = User.Identifier();

            return this.rentCore.Request(request, userId);
        }

        [HttpPost]
        public ItemRenting Accept(RentalAction accept)
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

            return this.rentCore.Accept(accept, userId);
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

            this.rentCore.Delete(delete, userId);
        }

        [HttpPost]
        public ItemRenting Reject(RentalAction reject)
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

            return this.rentCore.Reject(reject, userId);
        }

        [HttpPost]
        public ItemRenting Return(RentalReturned returned)
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

            return this.rentCore.Return(returned, userId);
        }
        #endregion
    }
}