// Copyright (c) 2018 Joseph D Poirier
// Distributable under the terms of The New BSD License
// that can be found in the LICENSE file.
using System;
using System.Threading;
using System.Diagnostics;

namespace Monitor
{
    class MonitorProcess
    {
        public static void Main(string[] args)
        {
            if (args == null || args.Length != 4)
            {
                Console.WriteLine("error, missing argument (order: miner executable, reboot true|false, sleep time in minutes, miner options), exiting...");
                System.Environment.Exit(1);
            }

            string miner_path = args[0];        // miner path and name, e.g. c:/eth/miner/ethminer.exe
            string reboot = args[1];            // reboot or just restart miner
            int minutes = int.Parse(args[2]);   // sleep time
            string miner_opts = args[3];        // miner options, must be enclosed in quotes

        Start:
            Process _process = new Process();
            _process.StartInfo.FileName = miner_path;
            _process.StartInfo.Arguments = miner_opts;

            Console.WriteLine("----- monitor starting with options:");
            Console.WriteLine("{0}", miner_opts);

            try
            {
                _process.Start();

                while (!_process.HasExited && _process.Responding)
                {
                    _process.Refresh();
                    Console.WriteLine("--- Total process time: {0}", _process.TotalProcessorTime);

                    Thread.Sleep(minutes * 60 * 1000);
                }

                Console.WriteLine("Process exit code: {0}", _process.ExitCode);
            }
            finally
            {
                if (_process != null)
                {
                    _process.Close();
                }
            }

            if (reboot != "true")
            {
                goto Start;
            }

            // reboot
            Process.Start("shutdown", "/r /t 0");
        }
    }
}
