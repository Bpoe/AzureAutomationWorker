namespace AzureAutomation.Common
{
    using System.Diagnostics;
    using Models;

    public class StreamHandler
    {
        private JobData jobData;
        private Process runtimeProcess;
        private JrdsClient jrdsClient;

        public StreamHandler(JobData jobData, Process runtimeProcess, JrdsClient jrdsClient)
        {
            this.jobData = jobData;
            this.runtimeProcess = runtimeProcess;
            this.jrdsClient = jrdsClient;
        }

        public void Run()
        {

        }

        public void ProcessDebugStream(int streamCount, string output)
        {
            this.SetStream(streamCount, "Debug", output);
        }

        public void ProcessErrorStream(int streamCount, string output)
        {
            this.SetStream(streamCount, "Error", output);
        }

        public void ProcessOutputStream(int streamCount, string output)
        {
            this.SetStream(streamCount, "Output", output);
        }

        public void ProcessVerboseStream(int streamCount, string output)
        {
            this.SetStream(streamCount, "Verbose", output);
        }

        public void ProcessWarningStream(int streamCount, string output)
        {
            this.SetStream(streamCount, "Warning", output);
        }
        public void SetStream(int streamCount, string streamType, string output)
        {
            this.jrdsClient.SetStream(this.jobData.JobId, this.jobData.RunbookVersionId, output, streamType, streamCount);
        }
    }
}
