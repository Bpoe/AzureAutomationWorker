using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace AzureAutomation.Common
{
    using System.Net.Http;

    public static class Extensions
    {
        public static HttpResponseMessage EnsureSuccessStatusCode(this HttpResponseMessage response, string message)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(message);
            }

            return response;
        }

        public static HttpWebResponse EnsureSuccessStatusCode(this HttpWebResponse response, string message)
        {
            var responseMessage = new HttpResponseMessage(response.StatusCode);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException(message);
            }

            return response;
        }
    }
}
