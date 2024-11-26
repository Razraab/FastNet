using FastNet.Infrastructure.Interfaces;

namespace FastNet.Infrastructure.Services
{
    public class FileLogger<TOwner> : ILogger<TOwner> where TOwner : class
    {
        public const string LogFileName = "fastnet.log";

        public void Reset()
        {
            File.Delete(LogFileName);
        }

        public void Log(object? message, LogLevel level)
        {
            using(StreamWriter writer = new StreamWriter(LogFileName, true))
            {
                string currentDateTime = DateTime.Now.ToString("g");
                string formattedString = string.Format("{0} - [{1}] ({2}): {3}", currentDateTime, level, typeof(TOwner).Name, message);
                writer.WriteLine(formattedString);
            }
        }
    }
}
