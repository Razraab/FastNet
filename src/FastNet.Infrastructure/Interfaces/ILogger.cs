namespace FastNet.Infrastructure.Interfaces
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }

    public interface ILogger<TOwner> where TOwner : class
    {
        public void Reset();
        public void Log(object? message, LogLevel level);
    }
}
