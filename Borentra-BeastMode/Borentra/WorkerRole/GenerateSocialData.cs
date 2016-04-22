namespace Borentra.WorkerRole
{
    using Borentra.DataAccessLayer;
    using System;

    /// <summary>
    /// Generate Social Data
    /// </summary>
    public class GenerateSocialData : ScheduledProcedure<AdminGenerateSocialData>
    {
        #region Members
        /// <summary>
        /// Reoccurrence
        /// </summary>
        private static readonly TimeSpan reoccurrence = TimeSpan.FromMinutes(1);
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public GenerateSocialData()
            : base(reoccurrence.TotalSeconds)
        {
        }
        #endregion
    }
}