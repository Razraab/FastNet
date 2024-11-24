namespace FastNet.UI.Core.Console.Commands
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
