using FastNet.Infrastructure.Interfaces;
using FastNet.Infrastructure.Network;
using System.Net;
using System.Text;

namespace FastNet.UI.Core.Console.Commands
{
    public class GetCommand : ConsoleCommand
    {
        private readonly ILogger<GetCommand> _logger;

        public GetCommand(ILogger<GetCommand> logger) : base("get")
        {
            _logger = logger;

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
            // Output the status code of the response
            InnerCommands.Add(new ConsoleCommand("headers", (args) =>
            {
                HttpResponse? response = args.Last() as HttpResponse;
                if (response != null && response.Headers != null)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach(var header in response.Headers)
                    {
                        sb.AppendLine($"{header.Key}: {string.Join(", ", header.Value)}");
                    }
                    ConsoleWrapper.Write(sb.ToString());
                }
            }));            
            // Output the status code of the response
            InnerCommands.Add(new ConsoleCommand("cookies", (args) =>
            {
                HttpResponse? response = args.Last() as HttpResponse;

                if (response != null)
                {
                    StringBuilder sb = new StringBuilder();
                    CookieCollection cookies = response.Cookies.GetAllCookies();
                    foreach (Cookie cookie in cookies)
                    {
                        sb.AppendLine($"{cookie.Name} : {cookie.Value} : {cookie.Expires}");
                    }
                    ConsoleWrapper.Write(sb.ToString());
                    ConsoleWrapper.NewLine();
                }
            }));
        }

        public override void Execute(object[] args)
        {
            if (args.Length < 2) return;

            // Create http request object
            HttpRequest request;
            HttpResponse response;
            try
            {
                request = new HttpRequest(args[1].ToString() ?? "");
                response = request.Get().Result;
            }
            catch (Exception ex)
            {
                ConsoleWrapper.WriteWithColor($"Error {ex.Message}", ConsoleColor.Red);
                ConsoleWrapper.NewLine();
                return;
            }

            // Create and start dialog
            ConsoleDialog dialog = new ConsoleDialog();
            dialog.Start((history) =>
            {
                string lastFullCommand = history.Last();
                object[] args = ((object[])lastFullCommand.Split(' '))
                    .Append(response)
                    .ToArray();
                InnerCommands.FirstOrDefault(c => c.Command == args[0]?.ToString()?
                    .ToLower()
                    .Trim())?.Execute(args);
            });
        }
    }
}
