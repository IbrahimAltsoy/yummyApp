using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Diagnostics;
using yummyApp.Application.Exceptions;
using yummyApp.Application.Services.Logger;
using Serilog;
using ILogger = Serilog.ILogger;
using System;
using yummyApp.Domain.Entities;


namespace yummyApp.Api.Infrastructure
{
        public class GlobalExceptionHandler : IExceptionHandler
        {
            private readonly IAppLogger _logger;
            private readonly IConfiguration _configuration;
            private readonly IWebHostEnvironment _environment;

            private static readonly Dictionary<Type, int> ExceptionStatusCodes = new()
        {
            { typeof(ValidationException), StatusCodes.Status400BadRequest },
            { typeof(NotFoundException), StatusCodes.Status404NotFound },
            { typeof(UnauthorizedAccessException), StatusCodes.Status401Unauthorized },
            { typeof(ForbiddenAccessException), StatusCodes.Status403Forbidden },
            { typeof(ServiceException), StatusCodes.Status500InternalServerError },
            { typeof(SqlException), StatusCodes.Status500InternalServerError }
        };

            public GlobalExceptionHandler(IAppLogger appLogger, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _logger = appLogger;
                _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
                _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            }

            public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception ex, CancellationToken cancellationToken)
        {         
            if (_environment.IsDevelopment())
                {
                    Console.WriteLine("⚠️ GlobalExceptionHandler devreye girdi!");
                }

                var exceptionType = ex.GetType();
                var statusCode = ExceptionStatusCodes.TryGetValue(exceptionType, out var code) ? code : StatusCodes.Status500InternalServerError;

                var problemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Title = GetTitleForStatusCode(statusCode),
                    Type = $"https://tools.ietf.org/html/rfc7231#section-{statusCode}",
                    Detail = _environment.IsDevelopment() ? ex.Message : "An unexpected error occurred. Please try again later.",
                    Instance = httpContext.Request.Path
                };

                if (_environment.IsDevelopment())
                {
                    problemDetails.Extensions["StackTrace"] = ex.StackTrace;
                    problemDetails.Extensions["InnerException"] = ex.InnerException?.Message;
                }

                httpContext.Response.StatusCode = statusCode;
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
         await _logger!.LogError("[{StatusCode}] Type: {ExceptionType}, Message: {ExceptionMessage}, Time: {DateTimeUtcNow}, StackTrace: {StackTrace}"
                  );
           

                return true;
            }

            private static string GetTitleForStatusCode(int statusCode) => statusCode switch
            {
                StatusCodes.Status400BadRequest => "Validation Error",
                StatusCodes.Status404NotFound => "Resource Not Found",
                StatusCodes.Status401Unauthorized => "Unauthorized",
                StatusCodes.Status403Forbidden => "Forbidden",
                StatusCodes.Status500InternalServerError => "Internal Server Error",
                _ => "Error"
            };
        }
 }



