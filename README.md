# miner-monitor
stupid simple crypto mining monitor that either reboots the machine or restarts the miner if it exits or stops responding


## Usage
Argument order:

    1. path to the miner executable
    2. true to reboot or false to just restart the miner on erros/hangs
    3. monitor thread sleep time in minutes
    4. miner executable options enclosed in quotes

E.g. using etherminer with dwarfpool and the monitor thread sleeps for 5 minutes, you'd place
something similar to the following in your boot-up script file:

    C:\eth\monitor\bin\Release\netcoreapp2.0\win10-x64\monitor.exe C:/eth/miner/ethminer.exe false 5 "--exit -U --cuda-schedule auto -P http://eth-us2.dwarfpool.com:80/0xgasdhjashjfghgfga/rig1 -P http://eth-us.dwarfpool.com:80/0xgasdhjashjfghgfga/rig1"


## Build example (assumes you have the dotnet sdk installed)
    Not sure what the best way of doing this is?

    $ cd miner-monitor
    $ dotnet new console -o .

    *delete the Program.cs file

    $ dotnet publish -c Release -r win10-x64 | linux-x64


## Misc
To get rid of error dialogue box prompts on Windows read:

https://www.raymond.cc/blog/disable-program-has-stopped-working-error-dialog-in-windows-server-2008/


Get ethminer binaries on github [here](https://github.com/ethereum-mining/ethminer/releases)


## Notes
Only tested on Windows 10.
