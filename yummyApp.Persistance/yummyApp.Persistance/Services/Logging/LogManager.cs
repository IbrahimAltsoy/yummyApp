using Microsoft.Extensions.Configuration;
using Serilog;
using yummyApp.Application.Services.Logger;

namespace yummyApp.Persistance.Services.Logging
{// Loglama işlemlerini Azure üzerine taşıyarak işlemler yapabilirsin bu durumu da göz önünde bulundur böyle yaparsan MangoDb üzerinden taşıdığın bütün MangoDb üzerinde oluşturduğun loglaa işlemini silebilirsin 
    // Eğer Mango Db üzerinden oluşturacakssan Longlama mekanizmasını ona göre mangoDb yapılanmasını oluştur buralar eksik bilgin olsun 
    public class LogManager : IAppLogger
    {
        readonly IConfiguration _configuration;

        public LogManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ILogger CreatePerformanceLogger()
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
                .WriteTo.File("logs/yummyapp-performance-log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public ILogger CreateMongoLogger()
        {
            if (_configuration["App:IsMongoActive"] == "true")
            {
                return new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .MinimumLevel.Warning()
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
                    .WriteTo.MongoDBBson(
                        _configuration["MongoDbSettings:ConnectionString"] + "/" + _configuration["MongoDbSettings:DatabaseName"],
                        _configuration["MongoDbSettings:LogCollection"])
                    .CreateLogger();
            }

            return Log.Logger;
        }
    }
}
