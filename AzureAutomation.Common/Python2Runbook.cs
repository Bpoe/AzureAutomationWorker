namespace AzureAutomation.Common
{
    public class Python2Runbook : Runbook
    {
        public Python2Runbook(RunbookData runbookData)
            : base(runbookData)
        {
            this.FileExtension = ".py";
        }
    }
}
