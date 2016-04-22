namespace Borentra.WorkerRole
{
    using Borentra.DataAccessLayer;
    using System;

    /// <summary>
    /// Set Top Geo Location
    /// </summary>
    public class SetTopGeoLocation : ScheduledProcedure<AdminSetTopGeoLocation>
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
        public SetTopGeoLocation()
            : base(reoccurrence.TotalSeconds)
        {
        }
        #endregion
    }
}