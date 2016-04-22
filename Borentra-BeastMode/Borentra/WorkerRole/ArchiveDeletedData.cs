namespace Borentra.WorkerRole
{
    using Borentra.DataAccessLayer;
    using System;

    /// <summary>
    /// Archive Deleted Data
    /// </summary>
    public class ArchiveDeletedData : ScheduledProcedure<AdminArchive>
    {
        #region Members
        /// <summary>
        /// Reoccurrence
        /// </summary>
        private static readonly TimeSpan reoccurrence = TimeSpan.FromDays(7);
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ArchiveDeletedData()
            : base(reoccurrence.TotalSeconds)
        {
        }
        #endregion
    }
}