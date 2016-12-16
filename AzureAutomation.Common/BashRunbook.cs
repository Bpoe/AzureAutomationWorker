namespace AzureAutomation.Common
{
    using Models;

    public class BashRunbook : Runbook
    {
        public BashRunbook(RunbookData runbookData) : base(runbookData)
        {
            this.FileExtension = ".sh";
        }
    }
}
