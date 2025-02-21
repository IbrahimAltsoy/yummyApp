using Serilog;

namespace yummyApp.Application.Services.Logger
{
    public interface IAppLogger
    {
        //ILogger CreateMongoLogger();
        //ILogger CreatePerformanceLogger();
        //ILogger CreateDatabaseLogger();
        Task LogInformation(string message);
        Task LogWarning(string message);
        Task LogError(string message, Exception? exception = null);
        Task LogCritical(string message, Exception? exception = null);
    }
}
