using FastNet.Infrastructure.Interfaces;
using FastNet.UI.Commands;

namespace FastNet.UI.Console
{
    /// <summary>
    /// Class that performs the function of typing 
    /// and executing commands in the console
    /// </summary>
    public class ConsoleUI
    {
        private readonly ILogger<ConsoleUI> _logger;

        public CommandCollection Commands { get; set; }
        public Func<string, bool> QuitHandler { get; set; } = (c) =>
        {
            if (c == "quit") return true;
            return false;
        };

        public ConsoleUI(ILogger<ConsoleUI> logger, IEnumerable<ConsoleCommand> commands)
        {
            _logger = logger;
            Commands = new CommandCollection(commands);
        }

        public void Show()
        {
            _logger.Log("Show UI and handle loop", LogLevel.Debug);
            while (true)
            {
                string strCommand = ConsoleWrapper.ReadLine().Trim().ToLower();
                string[] args = strCommand.Split(' ');

                if (QuitHandler(strCommand)) break;
                foreach (ConsoleCommand command in Commands)
                {
                    if (command.Command == args[0])
                        command.Execute(args);
                }
            }
            _logger.Log("UI Closing", LogLevel.Debug);
        }
    }
}
