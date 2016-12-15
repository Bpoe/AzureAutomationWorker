namespace AzureAutomation.Common
{
    public enum RunbookKind
    {
        Unknown = 0,
        Script = 1,
        Workflow = 2,
        Graph = 3,
        Configuration = 4,
        PowerShell = 5,
        PowerShellWorkflow = 6,
        GraphPowerShell = 7,
        GraphPowerShellWorkflow = 8,
        Python2 = 9,
        Python3 = 10,
        Bash = 11
    }
}
