namespace Borentra.DataStore
{
    using Borentra.DataAccessLayer;
    using Microsoft.WindowsAzure.Storage;
    using System.Configuration;

    /// <summary>
    /// Azure Storage
    /// </summary>
    public class AzureStorage
    {
        #region Members
        /// <summary>
        /// Data Connection String
        /// </summary>
        public const string DataConnectionStringKey = "DataConnectionString";

        /// <summary>
        /// Data Connection String
        /// </summary>
        public const string SicData2 = "SicData2";

        /// <summary>
        /// Data Connection String
        /// </summary>
        public const string SicData3 = "SicData3";

        /// <summary>
        /// Account
        /// </summary>
        protected readonly CloudStorageAccount account;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="account">Storage Account</param>
        public AzureStorage(StorageAccounts account = StorageAccounts.Default)
        {
            this.account = Get(account);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get Cloud Storage Account
        /// </summary>
        /// <param name="account">Account</param>
        /// <returns>Cloud Storage Account</returns>
        public static CloudStorageAccount Get(StorageAccounts account = StorageAccounts.Default)
        {
            var key = Key(account);
            return CloudStorageAccount.Parse(ConfigurationManager.AppSettings[key]);
        }

        /// <summary>
        /// Get Key
        /// </summary>
        /// <param name="account">Account</param>
        /// <returns>Configuration Key</returns>
        public static string Key(StorageAccounts account = StorageAccounts.Default)
        {
            switch (account)
            {
                case StorageAccounts.Administrative1:
                    return SicData3;
                case StorageAccounts.Offsite1:
                    return SicData2;
                default:
                    return DataConnectionStringKey;
            }
        }
        #endregion
    }
}