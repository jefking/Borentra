namespace Borentra.WorkerRole
{
    using Borentra.DataAccessLayer;
    using System;

    /// <summary>
    /// Stored Procedure which happens repeatedly
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ScheduledProcedure<T> : ScheduledManager
        where T : IStoredProc, new()
    {
        #region Constructors
        /// <summary>
        /// Scheduled Manager Constructor
        /// </summary>
        /// <param name="periodInSeconds">Period In Seconds</param>
        protected ScheduledProcedure(double periodInSeconds = 60)
            : base(periodInSeconds)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Execute Procedure
        /// </summary>
        public override void Execute()
        {
            var sproc = Activator.CreateInstance<T>();
            sproc.ExecuteNonQuery();
        }
        #endregion
    }
}