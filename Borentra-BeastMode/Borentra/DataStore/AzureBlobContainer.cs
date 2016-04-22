namespace Borentra.DataStore
{
    using Borentra.DataAccessLayer;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Microsoft.WindowsAzure.Storage.RetryPolicies;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Azure Blob Container
    /// </summary>
    public class AzureBlobContainer : AzureStorage
    {
        #region Members
        /// <summary>
        /// Cloud Blob Client
        /// </summary>
        private readonly CloudBlobClient client;

        /// <summary>
        /// Container Reference
        /// </summary>
        protected readonly CloudBlobContainer containerRef;
        #endregion

        #region Constructors
        /// <summary>
        /// Azure Blob Container
        /// </summary>
        /// <param name="account">Account</param>
        /// <param name="container">Container</param>
        public AzureBlobContainer(string container, StorageAccounts account = StorageAccounts.Default)
            : base(account)
        {
            if (string.IsNullOrWhiteSpace(container))
            {
                throw new ArgumentException("container");
            }

            this.client = base.account.CreateCloudBlobClient();
            client.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(1), 5);
            this.containerRef = this.client.GetContainerReference(container.ToLowerInvariant());
        }
        #endregion

        #region Methods
        public async Task<bool> Create()
        {
            return await this.containerRef.CreateIfNotExistsAsync();
        }
        /// <summary>
        /// Get Block Blob Reference
        /// </summary>
        /// <param name="objId">Object Id</param>
        /// <returns>Cloud Block Blob</returns>
        protected ICloudBlob GetBlockBlobReference(string objId)
        {
            return this.containerRef.GetBlockBlobReference(objId);
        }
        #endregion
    }
}