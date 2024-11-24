namespace FastNet.UI.Core.Console
{
    /// <summary>
    /// A class containing the basic functions of the 
    /// static Console class, and small additions
    /// </summary>
    public static class ConsoleWrapper
    {
        public static void Clear() => System.Console.Clear();
        public static void NewLine() => System.Console.WriteLine();
        public static string ReadLine() => System.Console.ReadLine() ?? " ";
        public static void Write(object? message) => System.Console.Write(message);

        public static void WriteWithColor(object? message, ConsoleColor foreground)
        {
            ConsoleColor oldForeground = System.Console.ForegroundColor;
            System.Console.ForegroundColor = foreground;
            Write(message);
            System.Console.ForegroundColor = oldForeground;
        }

        public static void WriteWithColor(object? message, ConsoleColor foreground, ConsoleColor background)
        {
            ConsoleColor oldForeground = System.Console.ForegroundColor;
            ConsoleColor oldBackground = System.Console.BackgroundColor;
            System.Console.ForegroundColor = foreground;
            System.Console.BackgroundColor = background;
            Write(message);
            System.Console.ForegroundColor = oldForeground;
            System.Console.BackgroundColor = oldBackground;
        }
    }
}
