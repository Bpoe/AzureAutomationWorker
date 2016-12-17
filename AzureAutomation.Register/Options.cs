using CommandLine.Text;

namespace AzureAutomation.Register
{
    using CommandLine;

    public class Options
    {
        [Option('r', "register", DefaultValue = false, HelpText = "Perform registration.")]
        public bool Register { get; set; }

        [Option('d', "deregister", DefaultValue = false, HelpText = "Perform deregistration.")]
        public bool Deregister { get; set; }

        [Option('u', "url", Required = true, HelpText = "The URL of the registration service.")]
        public string Url { get; set; }

        [Option('a', "accountkey", Required = true)]
        public string AccountKey { get; set; }

        [Option('w', "workergroupname", Required = true, HelpText = "The name of the worker group to register in.")]
        public string WorkerGroupName { get; set; }

        [Option('c', "cert", Required = true, HelpText = "The certificate to use for registration.")]
        public string Certificate { get; set; }

        [Option('k', "key", Required = true)]
        public string Key { get; set; }

        [Option('m', "machineid", Required = true, HelpText = "The machine's ID.")]
        public string MachineId { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
