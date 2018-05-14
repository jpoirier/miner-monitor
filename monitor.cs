// dotnet new console -o myApp
// dotnet publish -c Release --self-contained -r win10-x64 | linux-x64
using System;
using System.Threading;
using System.Diagnostics;

namespace ProcessSample
{
    class ProcessMonitorSample
    {
        public static void Main(string[] args)
        {
            string rig = "";
            string pool_1 = ""; // must include forward-slash at end of string
            string pool_2 = ""; // must include forward-slash at end of string
            string key = "";

            if (args == null || args.Length < 2)
            {
                Console.WriteLine("error, missing rig and key arguments, exiting...");
                System.Environment.Exit(1);
            }

            rig = args[0];
            key = args[1];

            Process ethminer = new Process();
            //ethminer.StartInfo.FileName = "D:/Users/thokk/z_projects/z_dev/gitlab.com/eth_monitor/ethminer.exe";
            ethminer.StartInfo.FileName = "C:/eth/miner/ethminer.exe";


            ethminer.StartInfo.Arguments = "-U --cuda-schedule auto -P " + pool_1 + key + "/" + rig +
                                          " -P " + pool_2 + key + "/" + rig;

            Console.WriteLine("----- monitor starting for {0}...", rig);
            try
            {
                // Start the process.
                ethminer.Start();

                // Display the process statistics until
                // the user closes the program.
                do
                {
                    if (!ethminer.HasExited)
                    {
                        Thread.Sleep(5000);

                        // Refresh the current process property values.
                        ethminer.Refresh();
                        Console.WriteLine();

                        // Display current process statistics.
                        Console.WriteLine("{0} {1}", ethminer.ToString(), rig);
                        Console.WriteLine("-------------------------------------");
                        Console.WriteLine("    user processor time       : {0}", ethminer.UserProcessorTime);
                        Console.WriteLine("    privileged processor time : {0}", ethminer.PrivilegedProcessorTime);
                        Console.WriteLine("    total processor time      : {0}", ethminer.TotalProcessorTime);

                        if (!ethminer.Responding)
                        {
                            Console.WriteLine("Status = Not Responding");
                            break;
                        }
                    }
                } while (!ethminer.WaitForExit(-1));

                Console.WriteLine();
                Console.WriteLine("Process exit code: {0}", ethminer.ExitCode);
            }
            finally
            {
                if (ethminer != null)
                {
                    ethminer.Close();
                }
            }

            //Console.WriteLine("exiting monitor...");
            Process.Start("shutdown", "/r /t 0");
        }
    }
}
