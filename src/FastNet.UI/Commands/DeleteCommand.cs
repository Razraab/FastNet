using FastNet.Infrastructure.Interfaces;
using FastNet.Infrastructure.Network;
using FastNet.UI.Console;

namespace FastNet.UI.Commands
{
    public class DeleteCommand : HttpMethodCommand
    {
        private readonly ILogger<GetCommand> _logger;

        public DeleteCommand(ILogger<GetCommand> logger) : base("delete")
        {
            _logger = logger;
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
                request.Cookies = SavedCookies;
                response = request.Delete().Result;
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
