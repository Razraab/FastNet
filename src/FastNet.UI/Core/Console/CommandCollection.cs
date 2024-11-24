using FastNet.UI.Core.Console.Commands;
using System.Collections.ObjectModel;

namespace FastNet.UI.Core.Console
{
    public class CommandCollection : Collection<ConsoleCommand>
    {
        public CommandCollection(IEnumerable<ConsoleCommand> commands)
        {
            foreach (ConsoleCommand command in commands)
                Add(command);
        }

        protected override void ClearItems() { }
        protected override void RemoveItem(int index) { }
    }
}
