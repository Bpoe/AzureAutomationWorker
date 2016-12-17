namespace AzureAutomation.Common
{
    using System.Net.Http;
    using System.Security.Cryptography.X509Certificates;

    public class HttpClientWithClientCertificates : HttpClient
    {
        private readonly WebRequestHandler _handler;

        public X509CertificateCollection ClientCertificates
        {
            get
            {
                return this._handler.ClientCertificates;
            }
        }

        public HttpClientWithClientCertificates() : this(new WebRequestHandler())
        {
        }

        public HttpClientWithClientCertificates(X509Certificate certificate) : this(new WebRequestHandler())
        {
            this._handler.ClientCertificates.Add(certificate);
        }

        private HttpClientWithClientCertificates(WebRequestHandler handler) : base(handler)
        {
            this._handler = handler;
        }
    }
}
