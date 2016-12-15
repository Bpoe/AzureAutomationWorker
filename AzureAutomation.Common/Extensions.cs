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
    }
}
