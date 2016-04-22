namespace Borentra.WorkerRole
{
    using Borentra.Core;
    using System;

    public class LocationIpIndexer : ScheduledManager
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
        public LocationIpIndexer()
            : base(reoccurrence.TotalSeconds)
        {
        }
        #endregion

        #region Methods
        public override void Execute()
        {
            var core = new GeoCore();
            core.StoreLocations();
        }
        #endregion
    }
}
