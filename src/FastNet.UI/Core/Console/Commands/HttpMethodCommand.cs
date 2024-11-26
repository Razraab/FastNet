using FastNet.Infrastructure.Network;
using System.Net;

namespace FastNet.UI.Core.Console.Commands
{
    public abstract class HttpMethodCommand : ConsoleCommand
    {
        private protected CookieContainer SavedCookies { get; set; } = new CookieContainer();

        public HttpMethodCommand(string command) : base(command)
        {
            // Output the contents of the response
            InnerCommands.Add(new ConsoleCommand("content", (args) =>
            {
                HttpResponse? response = args.Last() as HttpResponse;
                if (response != null)
                {
                    ConsoleWrapper.Write(response.Content);
                    ConsoleWrapper.NewLine();
                }
            }));
            // Output the status code of the response
            InnerCommands.Add(new ConsoleCommand("status", (args) =>
            {
                HttpResponse? response = args.Last() as HttpResponse;
                if (response != null)
                {
                    ConsoleWrapper.Write(response.StatusCode);
                    ConsoleWrapper.NewLine();
                }
            }));
            // Output the headers of the response
            InnerCommands.Add(new ConsoleCommand("headers", (args) =>
            {
                HttpResponse? response = args.Last() as HttpResponse;
                if (response != null && response.Headers != null)
                {
                    ConsoleTable table = new ConsoleTable(50);
                    List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
                    // Transfer the headers to the header variable for output in the table
                    foreach (var header in response.Headers)
                        headers.Add(new KeyValuePair<string, string>(header.Key, string.Join(", ", header.Value)));

                    table.SetData(headers);
                    ConsoleWrapper.Write(table.ToString());
                    ConsoleWrapper.NewLine();
                }
            }));
            // Output the header by key of the response
            InnerCommands.Add(new ConsoleCommand("header", (args) =>
            {
                if (args.Length >= 3)
                {
                    HttpResponse? response = args.Last() as HttpResponse;
                    string? headerKey = args[1].ToString();
                    if (headerKey != null && response != null)
                    {
                        // Look for the corresponding header
                        var header = response.Headers?.FirstOrDefault(h => h.Key.ToLower() == headerKey);

                        if (header != null && header.Value.Value != null) ConsoleWrapper.Write(string.Join(", ", header.Value.Value));
                        else ConsoleWrapper.Write("There is no such header");
                        ConsoleWrapper.NewLine();
                    }
                }
            }));
            // Output the cookies of the response
            InnerCommands.Add(new ConsoleCommand("cookies", (args) =>
            {
                HttpResponse? response = args.Last() as HttpResponse;
                if (response != null)
                {
                    ConsoleTable table = new ConsoleTable(40);
                    CookieCollection cookieCollection = response.Cookies.GetAllCookies();
                    List<Infrastructure.Network.Models.Cookie> cookies = new List<Infrastructure.Network.Models.Cookie>();

                    // Adding cookie from collection to models
                    foreach (System.Net.Cookie cookie in cookieCollection)
                        cookies.Add(new Infrastructure.Network.Models.Cookie(cookie.Name, cookie.Value) { Expires = cookie.Expires });

                    table.SetData(cookies);
                    ConsoleWrapper.Write(table.ToString());
                    ConsoleWrapper.NewLine();
                }
            }));
            // Output the cookie by name of the response
            InnerCommands.Add(new ConsoleCommand("cookie", (args) =>
            {
                if (args.Length >= 3)
                {
                    HttpResponse? response = args.Last() as HttpResponse;
                    string? cookieName = args[1].ToString();
                    if (cookieName != null && response != null)
                    {
                        var cookie = response.Cookies.GetAllCookies().FirstOrDefault(h => h.Name.ToLower() == cookieName);
                        if (cookie != null) ConsoleWrapper.Write(string.Join(", ", cookie.Value));
                        else ConsoleWrapper.Write("There is no such cookie");
                        ConsoleWrapper.NewLine();
                    }
                }
            }));
            // Download the content of the response
            InnerCommands.Add(new ConsoleCommand("download", (args) =>
            {
                HttpResponse? response = args.Last() as HttpResponse;
                string? outputFilename = "site.html";
                if (args.Length >= 3)
                    outputFilename = args[1].ToString();
                if (outputFilename == null || response == null) return;
                using (StreamWriter writer = new StreamWriter(outputFilename))
                    writer.Write(response.Content);

                ConsoleWrapper.Write($"Content from response saved to \"{outputFilename}\"");
                ConsoleWrapper.NewLine();
            }));
            // Remove cookies
            InnerCommands.Add(new ConsoleCommand("del-cookies", (args) =>
            {
                HttpResponse? response = args.Last() as HttpResponse;
                if (response != null)
                {
                    SavedCookies = new CookieContainer();
                    ConsoleWrapper.Write("Cookies deleted");
                    ConsoleWrapper.NewLine();
                }
            }));
        }
    }
}
