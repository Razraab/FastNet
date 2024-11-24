using System.Net;
using System.Net.Http.Headers;

namespace FastNet.Infrastructure.Network
{
    public class HttpResponse
    {
        public int StatusCode { get; set; }
        public string? Content { get; set; }
        public HttpHeaders? Headers { get; set; }
        public CookieContainer Cookies { get; set; } = new CookieContainer();
    }
}
