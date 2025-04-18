﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Security.Claims;
using System.Text;
using yummyApp.Api;
using yummyApp.Application;
using yummyApp.Infrastructure;
using yummyApp.Persistance;
using yummyApp.Persistance.Context;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using yummyApp.Domain.Identity;
using Hangfire;
using yummyApp.Application.BackGroundJobs;
using yummyApp.Api.Filters;
using Serilog.Events;
using yummyApp.Api.Infrastructure;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Persistance.Seeders;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using Serilog.Sinks.MSSqlServer;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.TestHost;

var builder = WebApplication.CreateBuilder(args);

#region Serilog Configuration
var columnOptions = new ColumnOptions();
columnOptions.TimeStamp.ColumnName = "TimeStamp";
columnOptions.TimeStamp.DataType = SqlDbType.DateTimeOffset;
columnOptions.TimeStamp.NonClusteredIndex = true;
columnOptions.Store.Remove(StandardColumn.Properties);
columnOptions.Store.Remove(StandardColumn.MessageTemplate);
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
    .MinimumLevel.Override("System", LogEventLevel.Error)
    .MinimumLevel.Error()
    .Enrich.FromLogContext()
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "LogEntries",
            AutoCreateSqlTable = true,
            SchemaName = "dbo"
        },
        columnOptions: columnOptions
    )
    .CreateLogger();
#endregion

builder.WebHost.UseUrls("https://localhost:7009"); // Mobilden giriş yapabilmek için eklendi.
builder.Host.UseSerilog();

#region Dependency Injection
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddHttpClient();
builder.Services.AddWebApiServices(builder.Configuration);
#endregion

#region Cors Configurationu
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT Authorization header kullanarak giriş yapın. Örn: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddIdentityCore<AppUser>(options =>
{
    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
})
    .AddEntityFrameworkStores<YummyAppDbContext>()
    .AddDefaultTokenProviders();

#region Hangfire Configuration
builder.Services.AddHangfire(config =>
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddHangfireServer();
#endregion

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(24); // Şifre sıfırlama token süresi 24 saat
});

#region Jwt Configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]!)))),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
            expires != null ? expires > DateTime.UtcNow : false,
        NameClaimType = ClaimTypes.NameIdentifier,
    };
});
#endregion

var app = builder.Build();
app.UseExceptionHandler(_ => { });

if (app.Environment.IsDevelopment())
{
    await app.InitializeDb();
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<IYummyAppDbContext>();
        var userSeeder = new UserSeeder();
        //await userSeeder.Seed(dbContext);
    }
}

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangfireAuthorizationFilter() }
});

RecurringJob.AddOrUpdate<UserDeletionJob>(
    x => x.RunScheduledUserDeletion(),
    Cron.Daily(3, 00)
);

app.UseHangfireServer();
app.UseStaticFiles();
app.UseSerilogRequestLogging(); // HTTP isteklerini logla

app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "YummyApp API V1");
    c.RoutePrefix = "swagger"; // Swagger UI'yi /api/swagger altında aç
});

//app.Use(async (context, next) =>
//{
//    if (context.Request.Path == "/")
//    {
//        context.Response.Redirect("/api/swagger");
//        return;
//    }
//    await next();
//});
app.MapControllers();
app.MapFallbackToFile("/app/index.html");
app.Run();