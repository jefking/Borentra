namespace Borentra.API.Models
{
    using Borentra.Models;
    using System;

    #region ISecuredToken
    public interface ISecuredToken
    {
        #region Properties
        /// <summary>
        /// Identifier for transport
        /// </summary>
        Guid Id
        {
            get;
        }

        /// <summary>
        /// Gets or sets Key
        /// </summary>
        string Key
        {
            get;
        }
        #endregion
    }
    #endregion

    #region IToken
    public interface IToken
    {
        #region Properties
        string AccessToken
        {
            get;
        }
        #endregion
    }
    #endregion
}