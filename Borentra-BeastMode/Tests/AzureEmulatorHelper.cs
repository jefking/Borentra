namespace Tests
{
    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Utility class that provides a way to start and stop the Azure compute and storage emulator
    /// </summary>
    public class AzureEmulatorHelper
    {
        #region Members

        // environment variable that sets the location for the Azure SDK local deployment ouput directory
        private const string ComputeEmulatorDirectoryEnvironmentVariable = "_CSRUN_STATE_DIRECTORY";

        // location for the Azure SDK local deployment ouput directory (no spaces allowed)
        private const string ComputeEmulatorDirectory = @"C:\dftmp";

        // location for the csrun.exe emulator application 
        private static string emulator = ConfigurationSettings.AppSettings["AzureEmulator"];
        #endregion

        #region Public Methods

        /// <summary>
        /// Starts up the Azure Storage emulator
        /// </summary>
        public static void StartAzureStorageEmulator()
        {
            // start storage on the azure emulator
            using (var process = new Process())
            {
                // retrieve the locations for the Azure csrun.exe emulator application and the Azure cspack.exe package application
                // note that this /devstore:start call could hang/freeze if the storage emulator is not initialized properly 
                // for a given user that this code runs under. See CP-3863 for more details, but essentially the following command
                // needs to be run as an administrator for the JenkinsDev user to have he correct permissions: 'DSInit.exe /user:resaas-cd\JenkinsDev'
                process.StartInfo = CreateProcessStartInfo(emulator, "/devstore:start");
                process.Start();

                WriteProcessOutput(process);
                process.WaitForExit();

                if (!process.ExitCode.Equals(0))
                {
                    throw new InvalidOperationException("Starting the Azure storage emulator and loading with a package failed");
                }
            }
        }

        /// <summary>
        /// Starts up the Azure Computer emulator
        /// </summary>
        public static void StartAzureComputeEmulator()
        {
            Trace.WriteLine("StartAzureComputeEmulator() Begins");

            // intialize environment variable
            System.Environment.SetEnvironmentVariable(ComputeEmulatorDirectoryEnvironmentVariable, ComputeEmulatorDirectory);

            // check environment variable
            try
            {
                var envValue = Environment.GetEnvironmentVariable(ComputeEmulatorDirectoryEnvironmentVariable);
                Trace.WriteLine(string.Format("Value of Environment Variable {0}: {1}", ComputeEmulatorDirectoryEnvironmentVariable, envValue));
            }
            catch (System.Security.SecurityException e)
            {
                throw new InvalidOperationException(string.Format("Error searching for Environment Variable {0}: {1}", ComputeEmulatorDirectoryEnvironmentVariable, e.Message));
            }

            // restart compute emulator
            ShutdownAzureComputeEmulator();

            using (var process = new Process())
            {
                process.StartInfo = CreateProcessStartInfo(emulator, ConfigurationSettings.AppSettings["AzureComputeStartupArguments"]);
                process.Start();

                WriteProcessOutput(process);
                process.WaitForExit();

                var standardOutput = process.StandardOutput.ReadToEnd();
                var standardError = process.StandardError.ReadToEnd();

                if (!process.ExitCode.Equals(0))
                {
                    var message = string.Format("\nStartInfo: {0} ExitCode: {1} StandardOutput: {2} StandardError: {3}", process.StartInfo.Arguments, process.ExitCode, standardOutput, standardError);
                    throw new InvalidOperationException("Starting the Azure compute emulator and loading with a package failed" + message);

                }
            }

            Trace.WriteLine("StartAzureComputeEmulator() Ends");
        }

        /// <summary>
        /// Shuts down the Azure Storage emulator
        /// </summary>
        public static void StopAzureStorageEmulator()
        {
            // shut down storage on the azure emulator
            using (var process = new Process())
            {
                process.StartInfo = CreateProcessStartInfo(emulator, "/devstore:shutdown");
                process.Start();

                WriteProcessOutput(process);
                process.WaitForExit();

                if (!process.ExitCode.Equals(0))
                {
                    throw new InvalidOperationException("Shutting down Azure storage emulator failed");
                }
            }
        }

        /// <summary>
        /// Removes all deployments from the compute emulator
        /// </summary>
        public static void RemoveAllAzureComputePackages()
        {
            // cleanly remove all the packages deployed to the compute emulator
            using (var process = new Process())
            {
                process.StartInfo = CreateProcessStartInfo(emulator, "/removeAll");
                process.Start();

                WriteProcessOutput(process);
                process.WaitForExit();

                if (!process.ExitCode.Equals(0))
                {
                    throw new InvalidOperationException("Removing all deployments from the compute emulator failed");
                }
            }
        }

        /// <summary>
        /// Forcibly shut down all of the azure emulators
        /// </summary>
        public static void StopAllAzureEmulatorServices()
        {
            foreach (var process in Process.GetProcessesByName("DFService"))
            {
                AzureEmulatorHelper.KillProcess(process);
                // the process isn't owned by the caller so we can't check its exit code
            }
        }

        /// <summary>
        /// Shuts down Azure compute emulator
        /// </summary>
        public static void ShutdownAzureComputeEmulator()
        {
            // cleanly remove all the packages deployed to the compute emulator
            using (var process = new Process())
            {
                process.StartInfo = CreateProcessStartInfo(emulator, "/devfabric:shutdown");
                process.Start();

                WriteProcessOutput(process);
                process.WaitForExit();

                if (!process.ExitCode.Equals(0))
                {
                    throw new InvalidOperationException("Shutting down Azure compute emulator failed");
                }
            }
        }


        /// <summary>
        /// Kill Process
        /// </summary>
        /// <param name="process">Process</param>
        private static void KillProcess(Process process)
        {
            try
            {
                while (!process.HasExited)
                {
                    process.Kill();
                    process.WaitForExit(5);
                }
            }
            catch (Win32Exception)
            {
            }
            catch (InvalidOperationException)
            {
            }
        }
        #endregion

        #region Helpers

        /// <summary>
        /// Create the process start information
        /// </summary>
        /// <param name="fileName">the file name of the application to start</param>
        /// <param name="arguments">the arguments to feed to the applicaiton upon startup</param>
        /// <returns>a fully configured process start info object</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Justification = "Testing")]
        private static ProcessStartInfo CreateProcessStartInfo(string fileName, string arguments)
        {
            var processStartInfo = new ProcessStartInfo(fileName, arguments);

            // do not launch the cmd shell (better user experience when launching tests)
            processStartInfo.UseShellExecute = false;
            processStartInfo.ErrorDialog = false;
            processStartInfo.CreateNoWindow = true;

            // allow redirection of output so we can write it out to the console
            processStartInfo.RedirectStandardError = true;
            processStartInfo.RedirectStandardOutput = true;

            return processStartInfo;
        }

        /// <summary>
        /// Write out the output from the process
        /// </summary>
        /// <param name="process">the process to capture output from and write it out</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Justification = "Testing")]
        private static void WriteProcessOutput(Process process)
        {
            var standardOutput = process.StandardOutput.ReadToEnd();
            if (!string.IsNullOrEmpty(standardOutput))
            {
                var header = "Standard Output:";

                if (Debugger.IsAttached)
                {
                    // prints to the 'Debug' output window
                    System.Diagnostics.Debug.WriteLine(header);
                    System.Diagnostics.Debug.WriteLine(standardOutput);
                }

                Console.WriteLine(header);
                Console.WriteLine(standardOutput);
            }

            var errorOutput = process.StandardError.ReadToEnd();
            if (!string.IsNullOrEmpty(errorOutput))
            {
                var header = "Standard Error:";

                if (Debugger.IsAttached)
                {
                    // prints to the 'Debug' output window
                    System.Diagnostics.Debug.WriteLine(header);
                    System.Diagnostics.Debug.WriteLine(errorOutput);
                }

                Console.WriteLine(header);
                Console.WriteLine(errorOutput);
            }
        }
        #endregion
    }
}
