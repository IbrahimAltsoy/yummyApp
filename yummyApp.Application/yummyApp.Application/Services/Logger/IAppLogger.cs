using Serilog;

namespace yummyApp.Application.Services.Logger
{
    public interface IAppLogger
    {
        //ILogger CreateMongoLogger();
        //ILogger CreatePerformanceLogger();
       ILogger CreateDatabaseLogger();
    }
}
