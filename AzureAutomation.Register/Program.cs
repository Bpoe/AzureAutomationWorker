namespace AzureAutomation.Register
{
    using System;
    using AzureAutomation.Common;
    using CommandLine;

    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            var argsAreValid = Parser.Default.ParseArguments(args, options);

            if (!argsAreValid || options.Register == options.Deregister)
            {
                Console.WriteLine(options.GetUsage());
                Environment.Exit(1);
            }

            var agent = new AgentServiceClient(
                options.Url,
                options.AccountKey,
                options.Certificate,
                options.Key,
                options.WorkerGroupName,
                options.MachineId);

            if (options.Register)
            {
                agent.RegisterWorker();
            }

            if (options.Deregister)
            {
                agent.DeregisterWorker();
            }
        }
    }
}
