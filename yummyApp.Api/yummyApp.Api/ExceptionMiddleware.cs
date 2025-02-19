using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Text.Json;
using yummyApp.Application.Exceptions;
using yummyApp.Application.Exceptions.AuthExceptions;
using yummyApp.Application.Services.Logger;
using ILogger = Serilog.ILogger;
namespace yummyApp.Api
{
    //public class ExceptionMiddleware
    //{
    //    private readonly RequestDelegate _next;
    //    private readonly ILogger _logger;
    //    private readonly IWebHostEnvironment _environment;

    //    public ExceptionMiddleware(RequestDelegate next, IAppLogger appLogger, IWebHostEnvironment environment)
    //    {
    //        _next = next;
    //        _logger = appLogger.CreateDatabaseLogger();
    //        _environment = environment ?? throw new ArgumentNullException(nameof(environment));
    //    }

    //    public async Task InvokeAsync(HttpContext context)
    //    {
    //        Console.WriteLine("⚠️ Exception Middleware Çalıştı!");

    //        try
    //        {
    //            await _next(context);               
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.Error(ex, "An unhandled exception has occurred.");
    //            await HandleExceptionAsync(context, ex);
    //        }
    //    }

    //    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    //    {
    //        context.Response.ContentType = "application/json";
    //        context.Response.StatusCode = ex switch
    //        {
    //            ValidationException => StatusCodes.Status400BadRequest,
    //            NotFoundException => StatusCodes.Status404NotFound,
    //            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
    //            ForbiddenAccessException => StatusCodes.Status403Forbidden,
    //            SqlException => StatusCodes.Status500InternalServerError,
    //            _ => StatusCodes.Status500InternalServerError
    //        };

    //        var response = new ProblemDetails
    //        {
    //            Status = context.Response.StatusCode,
    //            Title = ex.GetType().Name,
    //            Detail = _environment.IsDevelopment() ? ex.Message : "An error occurred. Please try again later.",
    //            Instance = context.Request.Path
    //        };

    //        if (_environment.IsDevelopment())
    //        {
    //            response.Extensions.Add("StackTrace", ex.StackTrace);
    //        }

    //        await context.Response.WriteAsJsonAsync(response);
    //    }
    //}
}
