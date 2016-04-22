namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.DataAccessLayer.Table;
    using Borentra.DataStore;
    using System;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Scheduled Task Core
    /// </summary>
    public class ScheduledTaskCore : TableStorage
    {
        #region Members
        /// <summary>
        /// Period of Timer
        /// </summary>
        private readonly TimeSpan period;

        /// <summary>
        /// Maximum Duration before Retry
        /// </summary>
        private readonly TimeSpan retryInterval = TimeSpan.FromMinutes(10);
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ScheduledTaskCore(TimeSpan period)
            : base("tasks", StorageAccounts.Administrative1)
        {
            if (TimeSpan.Zero >= period)
            {
                throw new ArgumentException("period");
            }
            
            this.period = period;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determine whether a new task needs to be executed
        /// </summary>
        /// <param name="entry">Scheduled Task Entry</param>
        /// <returns>True if need to execute, false if not</returns>
        public bool CheckForTask(ScheduledTaskEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException("entry");
            }

            var performTask = true;

            Trace.WriteLine(string.Format("{0} [{1}] Querying scheduled tasks table for the latest task.", DateTime.UtcNow, entry.ServiceName));

            // Peek the table first to determine if there's any task to execute
            // Query the table by partition key (type, year, month)
            var records = base.QueryByPartition<ScheduledTaskEntry>(entry.PartitionKey);

            if (records != null && records.Count() > 0)
            {
                var latest = records.OrderByDescending(x => x.StartTime).First();

                Trace.WriteLine(string.Format("{0} [{1}] Latest task found in table: Partition: {2} Id: {3} StartTime: {4} CompletionTime: {5}", DateTime.UtcNow, entry.ServiceName, latest.PartitionKey, latest.Identifier, latest.StartTime, latest.CompletionTime));

                // 1. If the latest task has been completed, then perform task if
                // - the latest task has been completed more than <period> ago, or
                // - the latest task was unsuccessful
                // 2. If the latest task has been started but not completed yet,
                // then perform the task if it has been started more than <backupRetryInterval> ago
                performTask = (latest.CompletionTime.HasValue) ?
                    DateTime.UtcNow.Subtract(latest.CompletionTime.Value) >= period || !latest.Successful :
                    DateTime.UtcNow.Subtract(latest.StartTime) >= retryInterval;
            }

            return performTask;
        }
        #endregion
    }
}