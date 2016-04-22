namespace Borentra.WorkerRole
{
    using Borentra.Core;
    using System;

    public class DeleteBingCache : ScheduledManager
    {
        #region Members
        /// <summary>
        /// Reoccurrence
        /// </summary>
        private static readonly TimeSpan reoccurrence = TimeSpan.FromDays(14);
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DeleteBingCache()
            : base(reoccurrence.TotalSeconds)
        {
        }
        #endregion

        #region Methods
        public override void Execute()
        {
            var core = new BingCore();
            core.BustCache();
        }
        #endregion
    }
}