namespace Borentra.Web
{
    using Borentra.Models;
    using System;

    /// <summary>
    /// RSS Interface
    /// </summary>
    public interface IRss : IDescriptive, IReferenceType
    {
        #region Properties
        string Link { get; }

        DateTime PublishedOn
        {
            get;
        }

        string Image
        {
            get;
        }
        #endregion
    }
}