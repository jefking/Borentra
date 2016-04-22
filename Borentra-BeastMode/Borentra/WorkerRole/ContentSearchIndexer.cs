namespace Borentra.WorkerRole
{
    using Borentra.Core;
    using System;

    /// <summary>
    /// Lucene Search Content Indexer
    /// </summary>
    public class ContentSearchIndexer : ScheduledManager
    {
        #region Members
        /// <summary>
        /// Reoccurrence
        /// </summary>
        private static readonly TimeSpan reoccurrence = TimeSpan.FromMinutes(30);
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ContentSearchIndexer()
            : base(reoccurrence.TotalSeconds)
        {
        }
        #endregion

        #region Methods
        public override void Execute()
        {
            var core = new LuceneCore();
            core.IndexContent();
        }
        #endregion
    }
}