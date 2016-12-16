namespace AzureAutomation.Common.Models
{
    using System;

    public class JobActionData
    {
        public MessageMetaData MessageMetaData;
        public string MessageSource;
        public Guid LockToken;
        public Guid JobId;
    }

    public class MessageMetaData
    {
        public string PopReceipt;
        public Guid MessageId;
    }
}
