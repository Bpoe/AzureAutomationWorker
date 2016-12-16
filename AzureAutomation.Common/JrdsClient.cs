namespace AzureAutomation.Common
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;

    public class JrdsClient
    {
        private string protocolVersion = "1.0";
        private string baseUri;
        private string accountId;
        private string hybridWorkerGroupName;
        private string machineId;

        protected HttpClient httpClient;
        

        public JrdsClient()
        {
            this.httpClient = new HttpClient();

            this.baseUri = string.Empty; // configuration.get_jrds_base_uri();
            this.accountId = string.Empty; // configuration.get_account_id();
            this.hybridWorkerGroupName = string.Empty; // configuration.get_hybrid_worker_name();
            this.machineId = string.Empty; // configuration.get_machine_id();
        }

        public SandboxActionData[] GetSandboxActions()
        {
            var url = this.baseUri + "/automationAccounts/" + this.accountId +
                      "/Sandboxes/GetSandboxActions?HybridWorkerGroupName=" + this.hybridWorkerGroupName +
                      "&api-version=" + this.protocolVersion;
            const string errorMessage = "Unable to get sandbox actions.";

            return this.GetAndDeserialize<SandboxActionData[]>(url, errorMessage).Result;
        }

        public JobActionData[] GetJobActions(string sandboxId)
        {

            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/Sandboxes/" + sandboxId +
                      "/jobs/getJobActions?api-version=" + this.protocolVersion;
            var request = this.httpClient.GetAsync(url).Result;

            // Stale sandbox
            if (request.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new JrdsSandboxTerminated();
            }

            request.EnsureSuccessStatusCode("Unable to get job actions. [status=" + request.StatusCode + "]");

            var content = request.Content.ReadAsStringAsync().Result;
            var jobActions = JsonConvert.DeserializeObject<JobActionData[]>(content);

            foreach (var jobAction in jobActions)
            {
                this.AcknowledgeJobAction(sandboxId, jobAction.MessageMetaData);
            }

            return jobActions;
        }

        public JobData GetJobData(string jobId)
        {
            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/jobs/" + jobId + "?api-version=" +
                      this.protocolVersion;
            const string errorMessage = "Unable to get job.";

            return this.GetAndDeserialize<JobData>(url, errorMessage).Result;
        }

        public JobUpdatableData GetJobUpdatableData(string jobId)
        {
            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/jobs/" + jobId +
                      "/getUpdatableData?api-version=" + this.protocolVersion;
            const string errorMessage = "Unable to get job.";

            return this.GetAndDeserialize<JobUpdatableData>(url, errorMessage).Result;
        }

        public RunbookData GetRunbookData(string runbookVersionId)
        {
            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/runbooks/" + runbookVersionId +
                "?api-version=" + this.protocolVersion;
            const string errorMessage = "Unable to get runbook.";

            return this.GetAndDeserialize<RunbookData>(url, errorMessage).Result;
        }

        public VariableData GetVariableAsset(string name)
        {
            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/variables/" + name +
                "?api-version=" + this.protocolVersion;
            const string errorMessage = "Unable to get variable.";

            return this.GetAndDeserialize<VariableData>(url, errorMessage).Result;
        }

        public CredentialData GetCredentialAsset(string name)
        {
            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/credentials/" + name +
                "?api-version=" + this.protocolVersion;
            const string errorMessage = "Unable to get credential.";

            return this.GetAndDeserialize<CredentialData>(url, errorMessage).Result;
        }

        public void AcknowledgeJobAction(string sandboxId, MessageMetaData messageMetadata)
        {
            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/Sandboxes/" + sandboxId +
                "/jobs/AcknowledgeJobActions?api-version=" + this.protocolVersion;
            const string errorMessage = "Unable to acknowledge job action.";

            var payload = "{ 'MessageMetadatas': '" + messageMetadata + "'}";
            this.PostObject(url, payload, errorMessage).Wait();
        }

        public void SetJobStatus(
            string sandboxId,
            string jobId,
            string jobStatus,
            bool isTerminal,
            string exception = null)
        {
            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/Sandboxes/" + sandboxId +
                "/jobs/" + jobId + "/changeStatus?api-version=" + this.protocolVersion;
            const string errorMessage = "Unable to set job status.";

            var payload = "{ 'exception': '" + exception + "', 'isFinalStatus': '" + isTerminal + "', 'jobStatus': '" + jobStatus + "'}";
            this.PostObject(url, payload, errorMessage).Wait();
        }

        public void SetStream(string jobId, string runbookVersionId, string streamText, string streamType, int sequenceNumber)
        {
            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/jobs/" + jobId + "/postJobStream?api-version=" + this.protocolVersion;
            const string errorMessage = "Unable to set streams.";

            var payload = "{" +
                          "'AccountId': '" + this.accountId + "'," +
                          "'JobId': '" + jobId + "'," +
                          "'RecordTime': '" + DateTime.UtcNow + "', " + //.isoformat()
                          "'RunbookVersionId': '" + runbookVersionId + "', " +
                          "'SequenceNumber': '" + sequenceNumber + "', " +
                          "'StreamRecord': '', " +
                          "'StreamRecordText': '" + streamText + "', " +
                          "'Type':'" + streamType + "' }";
            this.PostObject(url, payload, errorMessage).Wait();
        }

        public void SetLog(string eventId, string activityId, string logType, string args)
        {
            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/logs?api-version=" + this.protocolVersion;
            const string errorMessage = "Unable to set log.";

            var payload = "{" +
                          "'activityId': '" + activityId + "'," +
                          "'args': '" + args + "'," +
                          "'eventId': '" + eventId + "', " +
                          "'logType': '" + logType + "' }";
            this.PostObject(url, payload, errorMessage).Wait();
        }

        public void UnloadJob(
            string subscriptionId,
            string sandboxId,
            string jobId,
            bool isTest,
            DateTime startTime,
            long executiontime)
        {
            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/Sandboxes/" + sandboxId +
                "/jobs/" + jobId + "/unload?api-version=" + this.protocolVersion;
            const string errorMessage = "Unable to unload job.";
            var payload = "{" +
              "'executionTimeInSeconds': '" + executiontime + "'," +
              "'isTest': '" + isTest + "'," +
              "'jobId': '" + jobId + "', " +
              "'startTime': '" + startTime + "', " + // .isoformat()
              "'subscriptionId': '" + subscriptionId + "' }";
            this.PostObject(url, payload, errorMessage).Wait();
        }

        private async Task<T> GetAndDeserialize<T>(string url, string errorMessage)
        {
            var request = await this.httpClient.GetAsync(url);
            request.EnsureSuccessStatusCode(
                string.Format("{0} [status={1}]", errorMessage, request.StatusCode));

            var content = await request.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        private async Task PostObject<T>(string url, T payload, string errorMessage)
        {
            var content = new StringContent(JsonConvert.SerializeObject(payload));
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var request = await this.httpClient.PostAsync(url, content);
            request.EnsureSuccessStatusCode(
                string.Format("{0} [status={1}]", errorMessage, request.StatusCode));
        }
    }
}
