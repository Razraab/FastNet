using FastNet.UI.Console;

namespace FastNet.UI.Commands
{
    public class ClearCommand : ConsoleCommand
    {
        public ClearCommand() : base("clear") { }

        public override void Execute(object[] args)
        {
            ConsoleWrapper.Clear();
        }
    }
}
