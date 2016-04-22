﻿namespace Borentra.WorkerRole
{
    using Borentra.Core;
    using Borentra.DataAccessLayer.Table;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Scheduled Manager
    /// </summary>
    public abstract class ScheduledManager : Manager
    {
        #region Members
        /// <summary>
        /// Task Core
        /// </summary>
        private readonly ScheduledTaskCore taskCore;
        #endregion

        #region Constructors
        /// <summary>
        /// Scheduled Manager Constructor
        /// </summary>
        /// <param name="periodInSeconds">Period In Seconds</param>
        protected ScheduledManager(double periodInSeconds = 60)
            : base(60, periodInSeconds)
        {
            this.taskCore = new ScheduledTaskCore(TimeSpan.FromSeconds(periodInSeconds));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="state">State</param>
        protected override async void Execute(object state)
        {
            var startTime = DateTime.UtcNow;
            var completionTime = DateTime.UtcNow;

            var entry = new ScheduledTaskEntry(this.GetType().ToString());

            Trace.WriteLine(string.Format("{0} [{1}] Task Checking.", startTime, entry.ServiceName));

            try
            {
                if (taskCore.CheckForTask(entry))
                {
                    Trace.WriteLine(string.Format("{0} [{1}] Task Started.", DateTime.UtcNow, entry.ServiceName));

                    // Insert a new backup entry to table
                    entry.Identifier = Guid.NewGuid();
                    entry.StartTime = DateTime.UtcNow;

                    await taskCore.InsertOrReplace(entry);

                    try
                    {
                        this.Execute();
                        entry.Successful = true;
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(string.Format("{0}:{1}", this.GetType(), ex.Message));
                        entry.Successful = false;
                    }
                    finally
                    {
                        entry.CompletionTime = DateTime.UtcNow;
                    }

                    // Update entry in table
                    await taskCore.InsertOrReplace(entry);
                }
                else
                {
                    Trace.WriteLine(string.Format("{0} [{1}] No Action Required.", DateTime.UtcNow, entry.ServiceName));
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("{0}:{1}", this.GetType(), ex.Message));
                entry.Successful = false;
            }
            finally
            {
                completionTime = DateTime.UtcNow;
            }

            Trace.WriteLine(string.Format("{0} [{1}] Task Completed. Success: {2}", DateTime.UtcNow, entry.ServiceName, entry.Successful));
        }
        #endregion
    }
}