namespace Borentra.WorkerRole
{
    using Borentra.DataAccessLayer;
    using System;

    /// <summary>
    /// Set Top Location
    /// </summary>
    public class SetTopLocation : ScheduledProcedure<AdminSetTopLocation>
    {
        #region Members
        /// <summary>
        /// Reoccurrence
        /// </summary>
        private static readonly TimeSpan reoccurrence = TimeSpan.FromMinutes(5);
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SetTopLocation()
            : base(reoccurrence.TotalSeconds)
        {
        }
        #endregion
    }
}