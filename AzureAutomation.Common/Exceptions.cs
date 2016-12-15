namespace AzureAutomation.Common
{
    using System;

    public class JrdsSandboxTerminated : Exception
    {
        public JrdsSandboxTerminated() : this("JRDS sandbox terminated.")
        {
        }

        public JrdsSandboxTerminated(string message) : base(message)
        {
        }
    }

    public class SandboxRuntimeException : Exception
    {
        public SandboxRuntimeException() : base("Sandbox unhandled runtime exception.")
        {
        }

        public SandboxRuntimeException(string message) : base(message)
        {
        }
    }

    public class WorkerUnsupportedRunbookType : Exception
    {
        public WorkerUnsupportedRunbookType() : this("This runbook type isn't supported by Linux hybrid workers.")
        {
        }

        public WorkerUnsupportedRunbookType(string message) : base(message)
        {
        }
    }

    public class OsUnsupportedRunbookType : Exception
    {
        public OsUnsupportedRunbookType() : this("This runbook type isn't supported on this host.")
        {
        }

        public OsUnsupportedRunbookType(string message) : base(message)
        {
        }
    }

    public class InvalidRunbookSignature : Exception
    {
        public InvalidRunbookSignature() : this("The runbook signature is invalid.")
        {
        }

        public InvalidRunbookSignature(string message) : base(message)
        {
        }
    }
}
