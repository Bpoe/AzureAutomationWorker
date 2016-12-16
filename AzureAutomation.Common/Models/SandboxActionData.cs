namespace AzureAutomation.Common.Models
{
    using System;

    public class SandboxActionData
    {
        public Guid SandboxId;
        public string ConnectionEndpoint;
        public int ConnectionPort;
        public string SandboxCookie;
        public string CredentialName;
    }
}
