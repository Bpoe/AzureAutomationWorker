namespace AzureAutomation.Common.Models
{
    using System;
    using System.Collections.Generic;

    public class JobData
    {

        public string CreationTime; 

        public string EndTime; 

        public string StartRequestTime; 

        public string JobId; 

        public string JobKey; 

        public string JobStatus; 

        public string TriggerScope; 

        public string JobStatusDetails; 

        public string LastModifiedTime; 

        public string LastStatusModifiedTime; 

        public string PartitionId; 

        public IDictionary<string, string> Parameters; 

        public string InvokedRunbookVersions; 

        public string PendingAction; 

        public string PendingActionData; 

        public string RunbookVersionId; 

        public string RunbookVersion; 

        public string StartTime; 

        public string AccountId; 

        public string WorkflowInstanceId; 

        public string RunbookVersionKey; 

        public string RunbookKey; 

        public string AccountKey; 

        public string JobException; 

        public ModuleInfoData[] ModuleInfo; 

        public string FullWorkflowScript; 

        public string TrackingId; 

        public string SubscriptionId; 

        public string NoPersistEvictionsCount; 

        public string Status; 

        public string RunOn; 

        public string TierName; 

        public string AccountName; 

        public string TriggerSource; 
    }

    public class ModuleInfoData
    {
        public bool IsGlobal;
        public Guid ModuleId;
        public Guid ModuleVersionId;
        public string Name;
        public int VersionKey;
    }
}
