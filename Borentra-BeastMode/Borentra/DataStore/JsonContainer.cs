namespace Borentra.DataStore
{
    using Borentra.DataAccessLayer;
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Script.Serialization;

    /// <summary>
    /// Json Container
    /// </summary>
    public class JsonContainer : AzureBlobContainer
    {
        #region Members
        /// <summary>
        /// Content Type
        /// </summary>
        private const string contentType = "application/json";
        #endregion

        #region Constructors
        /// <summary>
        /// Json Container
        /// </summary>
        /// <param name="account">Account</param>
        /// <param name="container">Container</param>
        public JsonContainer(string container = "json", StorageAccounts account = StorageAccounts.Default)
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
        /// <returns>URI</returns>
        public async Task<Uri> Save(string objId, object content)
        {
            if (string.IsNullOrWhiteSpace(objId))
            {
                throw new ArgumentException("objId");
            }

            if (null == content)
            {
                throw new ArgumentNullException("content");
            }

            var serializer = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue,
            };

            var json = serializer.Serialize(content);
            var bytes = Encoding.UTF8.GetBytes(json);

            var blob = this.GetBlockBlobReference(objId);
            await blob.UploadFromByteArrayAsync(bytes, 0, bytes.Length);

            blob.Properties.ContentType = contentType;
            blob.SetProperties();

            return blob.Uri;
        }

        /// <summary>
        /// Get Item From Json
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="objId">Object Identifier</param>
        /// <returns>Item</returns>
        public T Get<T>(string objId)
        {
            var data = default(T);
            var blob = base.containerRef.GetBlockBlobReference(objId);
            if (blob.Exists())
            {
                var serializer = new JavaScriptSerializer();
                data = serializer.Deserialize<T>(blob.DownloadText());
            }

            return data;
        }
        #endregion
    }
}