using System.Reflection;

namespace FastNet.UI.Core.Console.Commands
{
    public class HelpCommand : ConsoleCommand
    {
        public HelpCommand() :base("help") { }
        private readonly string HelpMessage =
            """
            clear - clear the console
            get - http get request to address
            """;

        public override void Execute(object[] args)
        {
            ConsoleWrapper.Write(HelpMessage);
            ConsoleWrapper.NewLine();
        }
    }
}
