namespace AzureAutomation.Common
{
    public class Configuration
    {
        public const string ConfigEnvKey = "WORKERCONF";
        public const string WorkerRequiredConfigSection = "worker-required";
        public const string OptionalConfigSection = "worker-optional";

        // manually set configuration values
        public const string SourceDirectoryPath = "source_directory_path";
        public const string Component = "component";

        // required configuration keys
        public const string CertPath = "jrds_cert_path";
        public const string KeyPath = "jrds_key_path";
        public const string BaseUri = "jrds_base_uri";
        public const string AccountId = "account_id";
        public const string MachineId = "machine_id";
        public const string HybridWorkerGroupName = "hybrid_worker_group_name";
        public const string WorkerVersion = "worker_version";
        public const string WorkingDirectoryPath = "working_directory_path";

        // optional configuration keys
        public const string DebugTraces = "debug_traces";
        public const string BypassCertificateVerification = "bypass_certificate_verification";
        public const string EnforceRunbookSignatureValidation = "enforce_runbook_signature_validation";
        public const string GpgPublicKeyringPath = "gpg_public_keyring_path";
        public const string StateDirectoryPath = "state_directory_path";
        public const string JrdsPollingFrequency = "jrds_polling_frequency";
        public const string ProxyConfigurationPath = "proxy_configuration_path";

        // optional configuration default values

        public const string DefaultEmpty = "";
        public const string DefaultDebugTraces = "false";
        public const string DefautlBypassCertificateVerification = "false";
        public const string DefaultEnforceRunbookSignatureValidation = "true";
        public const string DefaultGpgPublicKeyringPath = DefaultEmpty;
        public const string DefaultStateDirectoryPath = DefaultEmpty;
        public const string DefaultProxyConfigurationPath = DefaultEmpty;
        public const string DefaultComponent = "Unknown";
        public const string DefaultWorkerVersion = "1.0.0.2";
        public const string DefaultJrdsPollingFrequency = "30";

        // state configuration keys
        public const string StatePid = "pid";
        public const string StateResourceVersion = "resource_version";
        public const string StateWorkspaceId = "workspace_id";
        public const string StateWorkerVersion = "worker_version";

        public void ReadAndSetConfiguration(string configPath)
        {

        }

        public void SetConfig(Configuration configuration)
        {
            
        }
    }
}
