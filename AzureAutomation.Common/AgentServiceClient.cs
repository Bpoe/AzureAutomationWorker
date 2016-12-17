namespace AzureAutomation.Common
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Cryptography.X509Certificates;
    using Newtonsoft.Json;

    public class AgentServiceClient
    {
        private string protocolVersion = "2.0";
        private string endpoint;
        private string accountKey;
        private string certPath;
        private string certKeyPath;
        private string workerGroupName;
        private string machineId;

        private HttpClient httpClient;

        public AgentServiceClient(
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

            var cert = new X509Certificate2(X509Certificate.CreateFromCertFile(this.certPath));
            this.httpClient = new HttpClientWithClientCertificates(cert);
        }

        public void RegisterWorker()
        {
            var cert = new X509Certificate2(X509Certificate.CreateFromCertFile(this.certPath));

            var payload = new AgentRegistrationData
            {
                RunbookWorkerGroup = this.workerGroupName,
                MachineName = ComputerInformation.FullyQualifiedDomainName,
                IpAddress = GetIpAddress(),
                Thumbprint = cert.Thumbprint,
                Issuer = cert.Issuer,
                Subject = cert.Subject,
            };

            var url = string.Format("{0}/HybridV2(MachineId='{1}')", this.endpoint, this.machineId);

            var content = new StringContent(JsonConvert.SerializeObject(payload));
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            content.Headers.Add("ProtocolVersion", this.protocolVersion);
            content.Headers.Add("x-ms-date", DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));

            var response = this.httpClient.PostAsync(url, content).Result;
            response.EnsureSuccessStatusCode(
                string.Format("Agentservice : Failed to register worker. [status={0}], [reasonPhrase={1}]", response.StatusCode, response.ReasonPhrase));
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

        private static string GetIpAddress()
        {
            var address = ComputerInformation.IpV4Addresses.FirstOrDefault();
            if (address != null)
            {
                return address.ToString();
            }

            address = ComputerInformation.IpV6Addresses.FirstOrDefault();
            if (address != null)
            {
                return address.ToString();
            }

            return null;
        }
    }

    public class AgentRegistrationData
    {
        public string RunbookWorkerGroup;
        public string MachineName;
        public string IpAddress;
        public string Thumbprint;
        public string Issuer;
        public string Subject;
    }
}
