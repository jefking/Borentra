namespace Borentra.DataStore
{
    using Borentra.DataAccessLayer;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Binary Container
    /// </summary>
    public class BinaryContainer : AzureBlobContainer
    {
        #region Constructors
        /// <summary>
        /// Binary Container
        /// </summary>
        /// <param name="container">Container</param>
        /// <param name="account">Account</param>
        public BinaryContainer(string container, StorageAccounts account = StorageAccounts.Default)
            : base(container, account)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="objId">Object Id</param>
        /// <param name="content">Content</param>
        /// <param name="contentType">Content Type</param>
        /// <returns>URI</returns>
        public async Task<ICloudBlob> Save(string objId, byte[] content, string contentType = "application/octet-stream")
        {
            if (string.IsNullOrWhiteSpace(objId))
            {
                throw new ArgumentException("objId");
            }

            var blob = this.GetBlockBlobReference(objId);
            await blob.UploadFromByteArrayAsync(content, 0, content.Length);

            blob.Properties.ContentType = contentType;
            await blob.SetPropertiesAsync();

            return blob;
        }

        /// <summary>
        /// Get Blob as byte[]
        /// </summary>
        /// <param name="objId">Object Id</param>
        /// <returns>Byte Array</returns>
        public byte[] Get(string objId)
        {
            if (string.IsNullOrWhiteSpace(objId))
            {
                throw new ArgumentException("objId");
            }

            var blob = this.GetBlockBlobReference(objId);
            byte[] bytes = null;
            using (var memoryStream = new MemoryStream())
            {
                blob.DownloadToStream(memoryStream);
                bytes = memoryStream.ToArray();
            }

            return bytes;
        }
        #endregion
    }
}