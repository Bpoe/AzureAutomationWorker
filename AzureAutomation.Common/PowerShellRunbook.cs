namespace AzureAutomation.Common
{
    using Models;

    public class PowerShellRunbook : Runbook
    {
        public PowerShellRunbook(RunbookData runbookData) : base(runbookData)
        {
            this.FileExtension = ".ps1";
        }
    }
}
