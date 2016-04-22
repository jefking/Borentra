namespace Borentra.WorkerRole
{
    using Borentra.Core;
    using System;

    /// <summary>
    /// Generate Heat Map Data
    /// </summary>
    public class GenerateHeatMap : ScheduledManager
    {
        #region Members
        /// <summary>
        /// Reoccurrence
        /// </summary>
        private static readonly TimeSpan reoccurrence = TimeSpan.FromDays(7);

        /// <summary>
        /// Data Core
        /// </summary>
        private readonly DataCore dataCore = new DataCore();
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public GenerateHeatMap()
            : base(reoccurrence.TotalSeconds)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Caches Country Percentages in JSON
        /// </summary>
        public override void Execute()
        {
            dataCore.CacheCountryPercentages();
        }
        #endregion
    }
}