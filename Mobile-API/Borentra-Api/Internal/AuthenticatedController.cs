namespace Borentra.API.Internal
{
    using System.Web.Http;

    public class AuthenticatedController : ApiController
    {
        #region Members
        /// <summary>
        /// Authenticator
        /// </summary>
        protected readonly Authenticator auth = new Authenticator();
        #endregion
    }
}