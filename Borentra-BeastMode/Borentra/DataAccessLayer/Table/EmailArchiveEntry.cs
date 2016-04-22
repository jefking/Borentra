namespace Borentra.DataAccessLayer.Table
{
    using Microsoft.WindowsAzure.Storage.Table;
    using System;

    public class EmailArchiveEntry : TableEntity
    {
        #region Properties
        public string From
        {
            get;
            set;
        }
        public string FromName
        {
            get;
            set;
        }
        public string To
        {
            get;
            set;
        }
        public string Subject
        {
            get;
            set;
        }
        public string Body
        {
            get;
            set;
        }
        public bool Sent
        {
            get;
            set;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generate the Partition & Row key
        /// </summary>
        public void GenerateKeys()
        {
            this.PartitionKey = string.Format("{0:yyyy}-{0:MM}", DateTime.UtcNow);
            this.RowKey = Guid.NewGuid().ToString();
        }
        #endregion
    }
}