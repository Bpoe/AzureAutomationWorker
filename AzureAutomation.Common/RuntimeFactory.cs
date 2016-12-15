namespace AzureAutomation.Common
{
    using System;

    public class RuntimeFactory
    {
        public Runtime CreateRuntime(JobData jobData, RunbookData runbookData)
        {
            var runbookDefinitionKind = runbookData.RunbookDefinitionKind;

            Runbook runbook;
            Runtime runtime;

            if (runbookDefinitionKind == RunbookKind.PowerShell)
            {
                runbook = new PowerShellRunbook(runbookData);
                runtime = new PowerShellRuntime(jobData, runbook);
            }
            else if (runbookDefinitionKind == RunbookKind.Python2)
            {
                runbook = new Python2Runbook(runbookData);
                runtime = new Python2Runtime(jobData, runbook);
            }
            else if (runbookDefinitionKind == RunbookKind.Python3)
            {
                runbook = new Python3Runbook(runbookData);
                runtime = new Python3Runtime(jobData, runbook);
            }
            else if (runbookDefinitionKind == RunbookKind.Bash)
            {
                runbook = new BashRunbook(runbookData);
                runtime = new BashRuntime(jobData, runbook);
            }
            else
            {
                throw new Exception();
            }

            if (!runtime.IsRuntimeSupported())
            {
                throw new Exception();
            }

            return runtime;
            //return runbook, runtime
        }
    }
}
