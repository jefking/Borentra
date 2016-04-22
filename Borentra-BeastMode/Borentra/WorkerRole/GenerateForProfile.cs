namespace Borentra.WorkerRole
{
    using Borentra.DataAccessLayer;
    using System;

    /// <summary>
    /// Generate For Profile
    /// </summary>
    public class GenerateForProfile : ScheduledProcedure<AdminGenerateForProfile>
    {
        #region Members
        /// <summary>
        /// Reoccurrence
        /// </summary>
        private static readonly TimeSpan reoccurrence = TimeSpan.FromMinutes(2);
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public GenerateForProfile()
            : base(reoccurrence.TotalSeconds)
        {
        }
        #endregion
    }
}