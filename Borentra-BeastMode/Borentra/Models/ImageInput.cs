namespace Borentra.Models
{
    using Borentra.Models;
    using System;

    public class ImageInput : IUserIdentifier, IIdentifier
    {
        #region Properties
        public Guid Identifier
        {
            get;
            set;
        }

        public Guid UserIdentifier
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public string ContentType
        {
            get;
            set;
        }

        public int FileSize
        {
            get;
            set;
        }

        public byte[] Contents
        {
            get;
            set;
        }
        #endregion
    }
}