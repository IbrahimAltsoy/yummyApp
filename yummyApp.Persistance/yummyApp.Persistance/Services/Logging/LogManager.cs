using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Data;
using yummyApp.Application.Services.Logger;

namespace yummyApp.Persistance.Services.Logging
{
    public class LogManager : IAppLogger
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;

        public LogManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _logger = Log.Logger;
        }

        private Task LogWithContextAsync(string level, string message, Exception? exception = null)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var userId = httpContext?.User?.FindFirst("uid")?.Value ?? "Anonymous";
            var ipAddress = httpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
            var endpoint = httpContext?.Request?.Path.Value ?? "Unknown";

            // 🔹 Log mesajını yapılandır
            var logger = _logger
                .ForContext("UserId", userId)
                .ForContext("IPAddress", ipAddress)
                .ForContext("Endpoint", endpoint)
                //.ForContext("Level", level)
                .ForContext("Message", message)
                //.ForContext("Exception", exception?.ToString())
                .ForContext("CreatedAt", DateTime.UtcNow);  // ⏳ Tarihi UTC olarak logla

            // 🔹 Loglama seviyesine göre uygun metodu çağır
            switch (level)
            {
                case "Information":
                    logger.Information("{Level}: {Message}", level, message);
                    break;
                case "Warning":
                    logger.Warning("{Level}: {Message}", level, message);
                    break;
                case "Error":
                    logger.Error(exception, "{Level}: {Message}", level, message);
                    break;
                case "Critical":
                    logger.Fatal(exception, "{Level}: {Message}", level, message);
                    break;
                default:
                    logger.Warning("Unknown log level: {Level}. Message: {Message}", level, message);
                    break;
            }

            Console.WriteLine($"✅ LOG KAYDEDİLDİ: {message} (Seviye: {level})");
            return Task.CompletedTask;
        }


        public Task LogInformation(string message) => LogWithContextAsync("Information", message);
        public Task LogWarning(string message) => LogWithContextAsync("Warning", message);
        public Task LogError(string message, Exception? exception = null)
            => LogWithContextAsync("Error", message, exception);
        public Task LogCritical(string message, Exception? exception = null)
            => LogWithContextAsync("Critical", message, exception);
    }
}

