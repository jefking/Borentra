namespace Borentra.WorkerRole
{
    using Borentra.DataAccessLayer;
    using System;

    /// <summary>
    /// Generate Badges
    /// </summary>
    public class GenerateBadges : ScheduledProcedure<AdminGenerateBadges>
    {
        #region Members
        /// <summary>
        /// Reoccurrence
        /// </summary>
        private static readonly TimeSpan reoccurrence = TimeSpan.FromHours(12);
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public GenerateBadges()
            : base(reoccurrence.TotalSeconds)
        {
        }
        #endregion
    }
}