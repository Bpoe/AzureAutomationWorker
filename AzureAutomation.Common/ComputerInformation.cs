namespace AzureAutomation.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;

    public class ComputerInformation
    {
        public static string FullyQualifiedDomainName
        {
            get
            {
                var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
                return string.Format("{0}.{1}", ipProperties.HostName, ipProperties.DomainName);
            }
        }

        public static IPAddress[] IpV4Addresses
        {
            get
            {
                return GetAllLocalIpAddresses(AddressFamily.InterNetwork);
            }
        }

        public static IPAddress[] IpV6Addresses
        {
            get
            {
                return GetAllLocalIpAddresses(AddressFamily.InterNetworkV6);
            }
        }

        private static IPAddress[] GetAllLocalIpAddresses(AddressFamily addressFamily)
        {
            var ipAddresses = new List<IPAddress>();

            var activeNicsMatchingType =
                NetworkInterface
                    .GetAllNetworkInterfaces()
                    .Where(i => i.NetworkInterfaceType == NetworkInterfaceType.Ethernet && i.OperationalStatus == OperationalStatus.Up);

            foreach (var item in activeNicsMatchingType)
            {
                ipAddresses.AddRange(
                    item.GetIPProperties()
                        .UnicastAddresses
                        .Where(ip => ip.Address.AddressFamily == addressFamily)
                        .Select(address => address.Address));
            }

            return ipAddresses.ToArray();
        }
    }
}
