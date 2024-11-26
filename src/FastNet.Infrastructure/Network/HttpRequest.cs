using System.Net;

namespace FastNet.Infrastructure.Network
{
    public class HttpRequest
    {
        public Uri Uri { get; set; }
        public CookieContainer Cookies { get; set; } = new CookieContainer();
        public object? Data { get; set; }

        public HttpRequest(Uri uri)
        {
            Uri = uri;
        }

        public HttpRequest(string url)
        {
            Uri = new Uri(url);
        }

        public async Task<HttpResponse> Get()
        {
            HttpResponse result = new HttpResponse();

            HttpClientHandler handler = new HttpClientHandler()
            {
                CookieContainer = Cookies,
                UseCookies = true,
                UseDefaultCredentials = true
            };

            using(HttpClient client = new HttpClient(handler))
            {
                HttpResponseMessage response = await client.GetAsync(Uri);

                result.StatusCode = (int)response.StatusCode;

                if (response.IsSuccessStatusCode)
                {
                    result.Content = await response.Content.ReadAsStringAsync();
                    result.Headers = response.Headers;
                    // Set cookies
                    if (response.Headers.Contains("Set-Cookie"))
                    {
                        foreach (string? cookieHeader in response.Headers.GetValues("Set-Cookie"))
                            result.Cookies.SetCookies(Uri, cookieHeader);
                    }
                }
            }
            return result;
        }

        public async Task<HttpResponse> Post(string data)
        {
            HttpResponse result = new HttpResponse();

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(data); 

                HttpResponseMessage response = await client.PostAsync(Uri, content);

                result.StatusCode = (int)response.StatusCode;

                if (response.IsSuccessStatusCode)
                {
                    result.Content = await response.Content.ReadAsStringAsync();
                    result.Headers = response.Headers;
                    // Set cookies
                    if (response.Headers.Contains("Set-Cookie"))
                    {
                        foreach (string? cookieHeader in response.Headers.GetValues("Set-Cookie"))
                            result.Cookies.SetCookies(Uri, cookieHeader);
                    }
                }
            }
            return result;
        }
    }
}
