namespace Borentra.WorkerRole
{
    using Borentra.Core;
    using System;

    /// <summary>
    /// Stores Home Page data in JSON
    /// </summary>
    public class GenerateHomePageData : ScheduledManager
    {
        #region Members
        /// <summary>
        /// Reoccurrence
        /// </summary>
        private static readonly TimeSpan reoccurrence = TimeSpan.FromDays(1);

        /// <summary>
        /// Data Core
        /// </summary>
        private readonly DataCore dataCore = new DataCore();
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public GenerateHomePageData()
            : base(reoccurrence.TotalSeconds)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Caching Home Page Data
        /// </summary>
        public override void Execute()
        {
            dataCore.CacheHomePageData();
        }
        #endregion
    }
}