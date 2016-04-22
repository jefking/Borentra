namespace BeastMode
{
    using Borentra.WorkerRole;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.WindowsAzure.Diagnostics;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Azure Worker Role
    /// </summary>
    public class WorkerRole : RoleEntryPoint
    {
        #region Members
        /// <summary>
        /// Services
        /// </summary>
        private IEnumerable<Manager> services = null;
        #endregion

        #region Methods
        /// <summary>
        /// Run Method
        /// </summary>
        public override void Run()
        {
            Trace.TraceInformation("Beast Mode run starting");

            try
            {
                Parallel.ForEach<Manager>(services, (service, state) =>
                {
                    var running = service.Run();
                    Trace.TraceInformation(string.Format("{0}: {1}", service.GetType(), running));
                });

                while (true)
                {
                    Thread.Sleep(10000);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("{0}", ex.Message));
            }

            Trace.TraceInformation("Beast Mode run exiting");
        }

        /// <summary>
        /// On Start Method
        /// </summary>
        /// <returns>Started</returns>
        public override bool OnStart()
        {
            var dmc = DiagnosticMonitor.GetDefaultInitialConfiguration();
            dmc.Logs.ScheduledTransferPeriod = TimeSpan.FromMinutes(1);
            dmc.Logs.ScheduledTransferLogLevelFilter = LogLevel.Verbose;
            //DiagnosticMonitor.Start("DataConnectionString", dmc);

            Trace.Write("Beast Mode, worker role On Start starting");

            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());

            try
            {
                if (null == this.services)
                {
                    Trace.Write("Loading services");

                    var toRun = new List<Manager>();
                    toRun.Add(new GenerateBadges());
                    toRun.Add(new GenerateForProfile());
                    toRun.Add(new ArchiveDeletedData());
                    toRun.Add(new GenerateSocialData());
                    toRun.Add(new GenerateHeatMap());
                    toRun.Add(new GenerateHomePageData());
                    toRun.Add(new SetTopGeoLocation());
                    toRun.Add(new SetTopLocation());
                    toRun.Add(new FacebookFriendGatherer());
                    toRun.Add(new ContentSearchIndexer());
                    toRun.Add(new LocationIpIndexer());
                    toRun.Add(new DeleteBingCache());
                    this.services = toRun;
                }
                else
                {
                    Trace.Write("Services already created.");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("{0}", ex.Message));
            }

            Trace.Write("Beast Mode, worker role OnStart ending");

            return base.OnStart();
        }

        /// <summary>
        /// On Stop
        /// </summary>
        public override void OnStop()
        {
            Trace.Write("Beast Mode OnStop starting");

            try
            {
                Parallel.ForEach<Manager>(services, (service, state) =>
                {
                    var stopped = service.Stop();
                    Trace.TraceInformation(string.Format("{0}: {1}", service.GetType(), stopped));
                });
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("{0}", ex.Message));
            }

            Trace.Write("Beast Mode OnStop ending");

            base.OnStop();
        }
        #endregion
    }
}