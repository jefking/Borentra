namespace Borentra.Code.Template
{
    using System.Collections.Generic;

    /// <summary>
    /// Dal Template Partial
    /// </summary>
    partial class Dal
    {
        #region Properties
        /// <summary>
        /// Gets or sets Manifest
        /// </summary>
        public Dictionary<string, Definition> Manifest
        {
            get;
            set;
        }
        #endregion
    }
}