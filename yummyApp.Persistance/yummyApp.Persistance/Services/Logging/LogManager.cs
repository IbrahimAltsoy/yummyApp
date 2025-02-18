using Microsoft.Extensions.Configuration;
using Serilog;
using System.Data;
using yummyApp.Application.Services.Logger;

namespace yummyApp.Persistance.Services.Logging
{ 
    public class LogManager : IAppLogger
    {
        readonly IConfiguration _configuration;

        public LogManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public ILogger CreatePerformanceLogger()
        //{
        //    return new LoggerConfiguration()
        //        .Enrich.FromLogContext()
        //        .MinimumLevel.Debug()
        //        .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
        //        .WriteTo.File("logs/yummyapp-performance-log.txt", rollingInterval: RollingInterval.Day)
        //        .CreateLogger();
        //}

        //public ILogger CreateMongoLogger()
        //{
        //    if (_configuration["App:IsMongoActive"] == "true")
        //    {
        //        return new LoggerConfiguration()
        //            .Enrich.FromLogContext()
        //            .MinimumLevel.Warning()
        //            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
        //            .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
        //            .WriteTo.MongoDBBson(
        //                _configuration["MongoDbSettings:ConnectionString"] + "/" + _configuration["MongoDbSettings:DatabaseName"],
        //                _configuration["MongoDbSettings:LogCollection"])
        //            .CreateLogger();
        //    }

        //    return Log.Logger;
        //}

        public ILogger CreateDatabaseLogger()
        {
            var columnOptions = new Serilog.Sinks.MSSqlServer.ColumnOptions();

            // 🔹 Varsayılan "Exception" sütununu tekrar eklememek için kaldırıyoruz.
            columnOptions.Store.Remove(Serilog.Sinks.MSSqlServer.StandardColumn.MessageTemplate);
            columnOptions.Store.Remove(Serilog.Sinks.MSSqlServer.StandardColumn.Properties);

            columnOptions.AdditionalColumns = new List<Serilog.Sinks.MSSqlServer.SqlColumn>
    {
        new Serilog.Sinks.MSSqlServer.SqlColumn { ColumnName = "UserId", DataType = SqlDbType.NVarChar, DataLength = 100, AllowNull = true },
        new Serilog.Sinks.MSSqlServer.SqlColumn { ColumnName = "IPAddress", DataType = SqlDbType.NVarChar, DataLength = 100, AllowNull = true },
        new Serilog.Sinks.MSSqlServer.SqlColumn { ColumnName = "Endpoint", DataType = SqlDbType.NVarChar, DataLength = 200, AllowNull = true }
    };

            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Warning() // Sadece Warning ve Error loglarını kaydet
                .WriteTo.MSSqlServer(
                    connectionString: _configuration["ConnectionStrings:DefaultConnection"],
                    sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                    {
                        TableName = "LogEntries",
                        AutoCreateSqlTable = false // 🔹 Eğer tablo migration ile oluşturulduysa, bunu false yap!
                    },
                    columnOptions: columnOptions)
                .CreateLogger();
        }



    }
}
