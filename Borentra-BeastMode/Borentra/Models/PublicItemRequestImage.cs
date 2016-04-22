namespace Borentra.Models
{
    using System;

    public class PublicItemRequestImage : PublicImage
    {
        #region Properties
        /// <summary>
        /// Item Request Identifier
        /// </summary>
        public Guid ItemRequestIdentifier
        {
            get;
            set;
        }
        #endregion
    }
}