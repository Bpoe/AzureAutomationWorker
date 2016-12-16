namespace AzureAutomation.Common
{
    using System;
    using System.Diagnostics;
    using Models;
    using Newtonsoft.Json;

    public class Runtime
    {
        protected string executionAlias = null;
        protected string baseCmd = null;

        protected JobData jobData;
        protected Runbook runbook;
        protected Process runbookSubProcess = null;

        public Runtime(JobData jobData, Runbook runbook)
        {
            this.jobData = jobData;
            this.runbook = runbook;
        }

        public void Initialize()
        {
            this.runbook.WriteToDisk();
        }

        public void StartRunbookSubProcess()
        {
            var cmd = this.baseCmd + this.runbook.FilePath;
            var jobParameters = this.jobData.Parameters;

            if (jobParameters != null && jobParameters.Count > 0)
            {
                foreach (var parameter in jobParameters)
                {
                    cmd += JsonConvert.DeserializeObject(parameter.Value);
                }
            }
        }

        public void KillRunbookSubProcess()
        {
            var maxAttempts = 3;

            for (var attempt =0; attempt < maxAttempts; attempt++)
            {
                if (this.runbookSubProcess != null)
                {
                    this.runbookSubProcess.Kill();
                    if (!Runtime.IsProcessAlive(this.runbookSubProcess))
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            throw new Exception();
        }

        public static bool IsProcessAlive(Process process)
        {
            return false;
        }

        public bool IsRuntimeSupported()
        {
            return false;
        }
    }

    public class PowerShellRuntime : Runtime
    {
        public PowerShellRuntime(JobData jobData, Runbook runbook)
            : base(jobData, runbook)
        {
            this.executionAlias = "powershell";
            this.baseCmd = this.executionAlias + "-File";
        }
    }

    public class Python2Runtime : Runtime
    {
        public Python2Runtime(JobData jobData, Runbook runbook)
            : base(jobData, runbook)
        {
            this.executionAlias = "python2";
            this.baseCmd = this.executionAlias;
        }
    }

    public class Python3Runtime : Runtime
    {
        public Python3Runtime(JobData jobData, Runbook runbook)
            : base(jobData, runbook)
        {
            this.executionAlias = "python3";
            this.baseCmd = this.executionAlias;
        }
    }

    public class BashRuntime : Runtime
    {
        public BashRuntime(JobData jobData, Runbook runbook)
            : base(jobData, runbook)
        {
        }
    }
}
