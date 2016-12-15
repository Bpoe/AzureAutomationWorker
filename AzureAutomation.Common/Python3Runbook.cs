namespace AzureAutomation.Common
{
    public class Python3Runbook : Runbook
    {
        public Python3Runbook(RunbookData runbookData) : base(runbookData)
        {
            this.FileExtension = ".py";
        }
    }
}
