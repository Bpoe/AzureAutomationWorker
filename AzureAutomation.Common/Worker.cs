namespace AzureAutomation.Common
{
    using System.Diagnostics;

    public class Worker
    {
        private string[] runningSandboxes;
        private JrdsClient jrdsClient;

        public Worker()
        {
            
        }

        public void Routine()
        {
            this.StopTrackingTerminatedSandbox();
        }

        public void MonitorSandboxProcessOutputs(string sandboxId, Process process)
        {
            
        }

        public void TelemetryRoutine()
        {
            
        }

        public void StopTrackingTerminatedSandbox()
        {
            
        }
    }
}
