namespace FastNet.UI.Commands
{
    public class ConsoleCommand
    {
        public string Command { get; private protected set; }
        private protected List<ConsoleCommand> InnerCommands { get; set; } = new List<ConsoleCommand>();

        private Action<object[]>? execute;

        public ConsoleCommand(string command)
        {
            Command = command;
        }

        public ConsoleCommand(string command, Action<object[]> execute)
        {
            Command = command;
            this.execute = execute;
        }

        public virtual void Execute(object[] args) => execute?.Invoke(args);
    }
}
