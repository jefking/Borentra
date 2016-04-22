namespace Borentra.API.Models
{
    using System;

    [Serializable]
    public class SecurityToken : ISecuredToken
    {
        #region Properties
        /// <summary>
        /// Gets or sets Identifier
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Validation Key
        /// </summary>
        public string Key
        {
            get;
            set;
        }
        #endregion
    }
}