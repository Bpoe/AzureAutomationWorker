namespace AzureAutomation.Common
{
    public class BashRunbook : Runbook
    {
        public BashRunbook(RunbookData runbookData) : base(runbookData)
        {
            this.FileExtension = ".sh";
        }
    }
}
