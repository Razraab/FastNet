namespace FastNet.UI.Core.Console
{
    public class ConsoleDialog
    {
        public string BeginString { get; set; } = ">> ";
        public List<string> History = new List<string>();

        public void Start(Action<List<string>> action) => Start(action, (string s) => s == "quit");

        public void Start(Action<List<string>> action, Func<string, bool> quitHandler)
        {
            while (true)
            {
                ConsoleWrapper.Write(BeginString);
                string command = ConsoleWrapper.ReadLine().Trim().ToLower();

                if (quitHandler(command)) break;

                History.Add(command);
                action(History);
            }
        }
    }
}
