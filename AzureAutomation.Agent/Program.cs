namespace AzureAutomation.Agent
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using AzureAutomation.Common;

    class Program
    {
        static void SafeLoop(Action action)
        {
            var sleepTime = TimeSpan.FromSeconds(30);
            while(true)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }

                Thread.Sleep(sleepTime);
            }
        }

        static void GenerateStateFile()
        {
            var stateFileName = "state.conf";

        }

        static void Main(string[] args)
        {
            var worker = new Worker();

            SafeLoop(worker.Routine);
        }
    }
}
