namespace Borentra.Models
{
    using System;

    public class PublicItemImage : PublicImage
    {
        #region Properties
        /// <summary>
        /// Item Identifier
        /// </summary>
        public Guid ItemIdentifier
        {
            get;
            set;
        }
        #endregion
    }
}