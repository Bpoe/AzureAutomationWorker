namespace AzureAutomation.Common
{
    using System;

    public class AgentService
    {
        private string protocolVersion = "2.0";
        private string endpoint;
        private string accountKey;
        private string certPath;
        private string certKeyPath;
        private string workerGroupName;
        private string machineId;

        public AgentService(
            string endpoint,
            string accountKey,
            string certPath,
            string certKeyPath,
            string workerGroupName,
            string machineId)
        {
            this.endpoint = endpoint;
            this.accountKey = accountKey;
            this.certPath = certPath;
            this.certKeyPath = certKeyPath;
            this.workerGroupName = workerGroupName;
            this.machineId = machineId.ToLower();
        }

        public void RegisterWorker()
        {
            
        }

        public void DeregisterWorker()
        {

        }

        private void ComputeHmac(DateTime date, string key, string payload)
        {

        }

        private static void EncodeBase64Sha256(string key, string message)
        {

        }
    }
}
