namespace AzureAutomation.Common
{
    using System.IO;

    public class Configuration
    {
        private const string ConfigEnvKey = "WORKERCONF";
        private const string WorkerRequiredConfigSection = "worker-required";
        private const string OptionalConfigSection = "worker-optional";

        // manually set configuration values
        private const string SourceDirectoryPath = "source_directory_path";
        private const string Component = "component";

        // required configuration keys
        private const string CertPath = "jrds_cert_path";
        private const string KeyPath = "jrds_key_path";
        private const string BaseUri = "jrds_base_uri";
        private const string AccountId = "account_id";
        private const string MachineId = "machine_id";
        private const string HybridWorkerGroupName = "hybrid_worker_group_name";
        private const string WorkerVersion = "worker_version";
        private const string WorkingDirectoryPath = "working_directory_path";

        // optional configuration keys
        private const string DebugTraces = "debug_traces";
        private const string BypassCertificateVerification = "bypass_certificate_verification";
        private const string EnforceRunbookSignatureValidation = "enforce_runbook_signature_validation";
        private const string GpgPublicKeyringPath = "gpg_public_keyring_path";
        private const string StateDirectoryPath = "state_directory_path";
        private const string JrdsPollingFrequency = "jrds_polling_frequency";
        private const string ProxyConfigurationPath = "proxy_configuration_path";

        // optional configuration default values
        private const string DefaultEmpty = "";
        private const string DefaultDebugTraces = "false";
        private const string DefautlBypassCertificateVerification = "false";
        private const string DefaultEnforceRunbookSignatureValidation = "true";
        private const string DefaultGpgPublicKeyringPath = DefaultEmpty;
        private const string DefaultStateDirectoryPath = DefaultEmpty;
        private const string DefaultProxyConfigurationPath = DefaultEmpty;
        private const string DefaultComponent = "Unknown";
        private const string DefaultWorkerVersion = "1.0.0.2";
        private const string DefaultJrdsPollingFrequency = "30";

        // state configuration keys
        private const string StatePid = "pid";
        private const string StateResourceVersion = "resource_version";
        private const string StateWorkspaceId = "workspace_id";
        private const string StateWorkerVersion = "worker_version";

        public void ReadAndSetConfiguration(string configPath)
        {

        }

        public void SetConfig(Configuration configuration)
        {
            
        }

        public string GetJrdsGetSandboxActionsPollingFreq()
        {
            return GetValue(JrdsPollingFrequency);
        }

        public string GetJrdsGetJobActionsPollingFreq()
        {
            return GetValue(JrdsPollingFrequency);
        }

        public string GetComponent()
        {
            return GetValue(Component);
        }

        public string GetJrdsCertPath()
        {
            return GetValue(CertPath);
        }

        public string GetJrdsKeyPath()
        {
            return GetValue(KeyPath);
        }

        public string GetJrdsBaseUri()
        {
            return GetValue(BaseUri);
        }

        public string GetAccountId()
        {
            return GetValue(AccountId);
        }

        public string GetMachineId()
        {
            return GetValue(MachineId);
        }

        public string GetHybridWorkerName()
        {
            return GetValue(HybridWorkerGroupName);
        }

        public string GetWorkerVersion()
        {
            return GetValue(WorkerVersion);
        }

        public string GetWorkingDirectoryPath()
        {
            return GetValue(WorkingDirectoryPath);
        }

        public string GetDebugTraces()
        {
            return GetValue(DebugTraces);
        }

        public string GetVerifyCertificates()
        {
            return GetValue(BypassCertificateVerification);
        }

        public string GetSourceDirectoryPath()
        {
            return GetValue(SourceDirectoryPath);
        }

        public string GetEnforceRunbookSignatureValidation()
        {
            return GetValue(EnforceRunbookSignatureValidation);
        }

        public string GetGpgPublicKeyringPath()
        {
            return GetValue(GpgPublicKeyringPath);
        }

        public string GetStateDirectoryPath()
        {
            return GetValue(StateDirectoryPath);
        }

        public string GetTemporaryRequestDirectoryPath()
        {
            return Path.Combine(this.GetWorkingDirectoryPath(), "req_tmp");
        }

        public string GetProxyConfigurationPath()
        {
            return GetValue(ProxyConfigurationPath);
        }

        private string GetValue(string value)
        {
            return string.Empty;
        }
    }
}
