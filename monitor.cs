// dotnet new console -o myApp
// dotnet publish -c Release -r win10-x64
using System;
using System.Threading;
using System.Diagnostics;

namespace ProcessSample
{
    class ProcessMonitorSample
    {
        public static void Main()
        {
            Process ethminer = new Process();
            //ethminer.StartInfo.FileName = "D:/Users/thokk/z_projects/z_dev/gitlab.com/eth_monitor/ethminer.exe";
            ethminer.StartInfo.FileName = "C:/eth/miner/ethminer.exe";

            string rig = "rig2";
            ethminer.StartInfo.Arguments = "-U --cuda-schedule auto -F http://eth-us2.dwarfpool.com:80/0xB1129EAF784d2598855AAa661D617Ac4dF09D24F/rig1 -FF http://eth-us.dwarfpool.com:80/0xB1129EAF784d2598855AAa661D617Ac4dF09D24F/"+rig;

            Console.WriteLine("----- monitor starting for {0}...", rig);
            try {
                // Start the process.
               ethminer.Start();

                // Display the process statistics until
                // the user closes the program.
                do {
                    if (!ethminer.HasExited) {
                        Thread.Sleep(5000);

                        // Refresh the current process property values.
                        ethminer.Refresh();
                        Console.WriteLine();

                        // Display current process statistics.
                        Console.WriteLine("{0} {1} -", ethminer.ToString(), rig);
                        Console.WriteLine("-------------------------------------");
                        Console.WriteLine("    user processor time: {0}", ethminer.UserProcessorTime);
                        Console.WriteLine("    privileged processor time: {0}", ethminer.PrivilegedProcessorTime);
                        Console.WriteLine("    total processor time: {0}", ethminer.TotalProcessorTime);

                        if (!ethminer.Responding) {
                            Console.WriteLine("Status = Not Responding");
                            break;
                        }
                    }
                } while (!ethminer.WaitForExit(-1));


                Console.WriteLine();
                Console.WriteLine("Process exit code: {0}", ethminer.ExitCode);
            } finally {
                if (ethminer != null) {
                    ethminer.Close();
                }
            }

            //Console.WriteLine("exiting monitor...");
            Process.Start("shutdown", "/r /t 0");
        }
    }
}
