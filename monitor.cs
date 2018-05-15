// dotnet new console -o monitor
// dotnet publish -c Release -r win10-x64 | linux-x64
using System;
using System.Threading;
using System.Diagnostics;

namespace ProcessSample
{
    class ProcessMonitorSample
    {
        public static void Main(string[] args)
        {
            if (args == null || args.Length != 2)
            {
                Console.WriteLine("error, missing argument string, exiting...");
                System.Environment.Exit(1);
            }

            Process _process = new Process();
            _process.StartInfo.FileName = args[0];  // miner path and name, e.g. c:/eth/miner/ethminer.exe
            _process.StartInfo.Arguments = args[1]; // miner options, must be enclosed in quotes

            Console.WriteLine("----- monitor starting with options:");
            Console.WriteLine("{0}", args[1]);

            try
            {
                _process.Start();

                while (!_process.HasExited && _process.Responding)
                {
                    _process.Refresh();
                    Console.WriteLine();
                    Console.WriteLine("--- Total process time: {0}", _process.TotalProcessorTime);

                    Thread.Sleep(1 * 60 * 1000);
                }

                Console.WriteLine();
                Console.WriteLine("Process exit code: {0}", _process.ExitCode);
            }
            finally
            {
                if (_process != null)
                {
                    _process.Close();
                }
            }

            // reboot
            Process.Start("shutdown", "/r /t 0");
        }
    }
}
