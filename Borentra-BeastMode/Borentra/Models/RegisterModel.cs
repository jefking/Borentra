namespace Borentra.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Register Model
    /// </summary>
    public class RegisterModel
    {
        #region Properties
        /// <summary>
        /// Gets or sets Name Identifier
        /// </summary>
        public string NameIdentifier { get; set; }

        /// <summary>
        /// Gets or sets UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether RememberMe
        /// </summary>
        public bool RememberMe { get; set; }
        #endregion
    }
}