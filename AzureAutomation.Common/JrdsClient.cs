namespace AzureAutomation.Common
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
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

        public string GetSandboxActions()
        {
            var url = this.baseUri + "/automationAccounts/" + this.accountId +
                      "/Sandboxes/GetSandboxActions?HybridWorkerGroupName=" + this.hybridWorkerGroupName +
                      "&api-version=" + this.protocolVersion;

            var request = this.httpClient.GetAsync(url).Result;
            request.EnsureSuccessStatusCode("Unable to get sandbox actions. [status=" + request.StatusCode + "]");

            return request.Content.ReadAsStringAsync().Result;
            // return request.deserialized_data["value"]
        }

        public string GetJobActions(string sandboxId)
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

            var jobActions = request.Content.ReadAsStringAsync().Result;

            if (true) // if len(job_actions) != 0:
            {
                var messageMetadata = string.Empty; // message_metadatas = [action["MessageMetadata"] for action in job_actions]
                this.AcknowledgeJobAction(sandboxId, messageMetadata);
            }

            return jobActions;
        }

        public JobData GetJobData(string jobId)
        {
            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/jobs/" + jobId + "?api-version=" +
                      this.protocolVersion;
            var request = this.httpClient.GetAsync(url).Result;
            request.EnsureSuccessStatusCode("Unable to get job. [status=" + request.StatusCode + "]");
            return JsonConvert.DeserializeObject<JobData>(request.Content.ReadAsStringAsync().Result);
        }

        public JobUpdatableData GetJobUpdatableData(string jobId)
        {
            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/jobs/" + jobId +
                      "/getUpdatableData?api-version=" + this.protocolVersion;

            var request = this.httpClient.GetAsync(url).Result;

            request.EnsureSuccessStatusCode("Unable to get job. [status=" + request.StatusCode + "]");
            return JsonConvert.DeserializeObject<JobUpdatableData>(request.Content.ReadAsStringAsync().Result);
        }

        public RunbookData GetRunbookData(string runbookVersionId)
        {
            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/runbooks/" + runbookVersionId +
                "?api-version=" + this.protocolVersion;

            var request = this.httpClient.GetAsync(url).Result;

            request.EnsureSuccessStatusCode("Unable to get runbook. [status=" + request.StatusCode + "]");
            return JsonConvert.DeserializeObject<RunbookData>(request.Content.ReadAsStringAsync().Result);
        }

        public string GetVariableAsset(string name)
        {
            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/variables/" + name +
                "?api-version=" + this.protocolVersion;

            var request = this.httpClient.GetAsync(url).Result;

            request.EnsureSuccessStatusCode("Unable to get variable. [status=" + request.StatusCode + "]");
            return request.Content.ReadAsStringAsync().Result;
        }

        public string GetCredentialAsset(string name)
        {
            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/credentials/" + name +
                "?api-version=" + this.protocolVersion;

            var request = this.httpClient.GetAsync(url).Result;

            request.EnsureSuccessStatusCode("Unable to get credential. [status=" + request.StatusCode + "]");
            return request.Content.ReadAsStringAsync().Result;
        }

        public void AcknowledgeJobAction(string sandboxId, string messageMetadata)
        {
            var payload = "{ 'MessageMetadatas': '" + messageMetadata + "'}";

            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/Sandboxes/" + sandboxId +
                "/jobs/AcknowledgeJobActions?api-version=" + this.protocolVersion;

            var content = new StringContent(payload);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var request = this.httpClient.PostAsync(url, content).Result;
            request.EnsureSuccessStatusCode("Unable to acknowledge job action. [status=" + request.StatusCode + "]");
        }

        public void SetJobStatus(
            string sandboxId,
            string jobId,
            string jobStatus,
            bool isTerminal,
            string exception = null)
        {
            var payload = "{ 'exception': '" + exception + "', 'isFinalStatus': '" + isTerminal + "', 'jobStatus': '" + jobStatus + "'}";

            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/Sandboxes/" + sandboxId +
                "/jobs/" + jobId + "/changeStatus?api-version=" + this.protocolVersion;

            var content = new StringContent(payload);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var request = this.httpClient.PostAsync(url, content).Result;
            request.EnsureSuccessStatusCode("Unable to set job status. [status=" + request.StatusCode + "]");
        }

        public void SetStream(string jobId, string runbookVersionId, string streamText, string streamType, int sequenceNumber)
        {
            var payload = "{" +
                          "'AccountId': '" + this.accountId + "'," +
                          "'JobId': '" + jobId + "'," +
                          "'RecordTime': '" + DateTime.UtcNow + "', " + //.isoformat()
                          "'RunbookVersionId': '" + runbookVersionId + "', " +
                          "'SequenceNumber': '" + sequenceNumber + "', " +
                          "'StreamRecord': '', " +
                          "'StreamRecordText': '" + streamText + "', " +
                          "'Type':'" + streamType + "' }";


            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/jobs/" + jobId + "/postJobStream?api-version=" + this.protocolVersion;

            var content = new StringContent(payload);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var request = this.httpClient.PostAsync(url, content).Result;
            request.EnsureSuccessStatusCode("Unable to set streams. [status=" + request.StatusCode + "]");
        }

        public void SetLog(string eventId, string activityId, string logType, string args)
        {
            var payload = "{" +
                          "'activityId': '" + activityId + "'," +
                          "'args': '" + args + "'," +
                          "'eventId': '" + eventId + "', " +
                          "'logType': '" + logType + "' }";


            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/logs?api-version=" + this.protocolVersion;

            var content = new StringContent(payload);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var request = this.httpClient.PostAsync(url, content).Result;
            request.EnsureSuccessStatusCode("Unable to set log. [status=" + request.StatusCode + "]");
        }

        public void UnloadJob(
            string subscriptionId,
            string sandboxId,
            string jobId,
            bool isTest,
            DateTime startTime,
            long executiontime)
        {
            var payload = "{" +
              "'executionTimeInSeconds': '" + executiontime + "'," +
              "'isTest': '" + isTest + "'," +
              "'jobId': '" + jobId + "', " +
              "'startTime': '" + startTime + "', " + // .isoformat()
              "'subscriptionId': '" + subscriptionId + "' }";


            var url = this.baseUri + "/automationAccounts/" + this.accountId + "/Sandboxes/" + sandboxId +
                "/jobs/" + jobId + "/unload?api-version=" + this.protocolVersion;

            var content = new StringContent(payload);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var request = this.httpClient.PostAsync(url, content).Result;
            request.EnsureSuccessStatusCode("Unable to unload job. [status=" + request.StatusCode + "]");
        }
    }
}
