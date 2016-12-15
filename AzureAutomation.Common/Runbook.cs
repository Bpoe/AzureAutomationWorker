namespace AzureAutomation.Common
{
    using System.IO;
    using System.Text;

    public class Runbook
    {
        protected readonly RunbookData RunbookData;

        protected RunbookKind DefinitionKind;

        protected string DefinitionKindString;

        protected string RunbookFilePath;

        protected string FileExtension;

        public string FilePath;

        public Runbook(RunbookData runbookData)
        {
            this.RunbookData = runbookData;
            this.DefinitionKind = runbookData.RunbookDefinitionKind;
            this.DefinitionKindString = runbookData.RunbookDefinitionKind.ToString();
            this.FilePath = string.Empty;
            this.FileExtension = string.Empty;
        }

        private void Initialize()
        {
            this.WriteToDisk();
            this.ValidateSignature();
        }

        private void WriteToDisk()
        {
            var fileName = this.RunbookData.Name + this.RunbookData.RunbookVersionId + this.FileExtension;
            var workingDirectory = string.Empty;
            this.RunbookFilePath = Path.Combine(workingDirectory, fileName);
            File.WriteAllText(this.FilePath, this.RunbookData.Definition, Encoding.UTF8);
        }

        private void ValidateSignature()
        {
        }
    }
}
