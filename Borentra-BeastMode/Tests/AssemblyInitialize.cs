namespace Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TestClass]
    public class AssemblyInitialize
    {
        #region Methods
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            DateTime startTime = DateTime.UtcNow;

            AzureEmulatorHelper.StartAzureStorageEmulator();

            // print out how long this method took to execute
            Trace.WriteLine(string.Format("Initialize() Elapsed Time: {0}", DateTime.UtcNow - startTime));
        }

        /// <summary>
        /// Clean-Up
        /// </summary>
        [AssemblyCleanup]
        public static void Cleanup()
        {
            DateTime startTime = DateTime.UtcNow;
            
            AzureEmulatorHelper.StopAllAzureEmulatorServices();

            // print out how long this method took to execute
            Trace.WriteLine(string.Format("Cleanup() Elapsed Time: {0}", DateTime.UtcNow - startTime));
        }
        #endregion
    }
}