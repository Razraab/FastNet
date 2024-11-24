using FastNet.Domain.Core;

namespace FastNet.Domain
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Application app = Application.Create("FastNet");
            app.Configure();
            app.Run();
        }
    }
}
