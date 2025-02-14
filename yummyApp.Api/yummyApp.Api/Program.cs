using Microsoft.AspNetCore.Authentication.JwtBearer;
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

#region Serilog Configuration
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .MinimumLevel.Warning()
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.File(
        "logs/yummyapp-log.txt",
        rollingInterval: RollingInterval.Day,
        fileSizeLimitBytes: 10 * 1024 * 1024,
        retainedFileCountLimit: 5,
        rollOnFileSizeLimit: true,
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
    .CreateLogger();
#endregion

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:7009"); // burası mobilden giriş yapabilmek için eklendi.
builder.Host.UseSerilog();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddHttpClient();
builder.Services.AddWebApiServices(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "YummyApp API", Version = "v1" });
});
builder.Services.AddIdentityCore<AppUser>(options =>
{
    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
})
    .AddEntityFrameworkStores<YummyAppDbContext>()
    .AddDefaultTokenProviders(); // Şifre sıfırlama ve e-posta doğrulama için gerekli
//Hangfire işlemleri için
builder.Services.AddHangfire(config =>
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")) 
);
builder.Services.AddHangfireServer();
// Şifre sıfırlama token süresini uzat
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(24); // Şifre sıfırlama token süresi 3 saat
});
// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]!)),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
            NameClaimType = ClaimTypes.NameIdentifier,
        };
    });

var app = builder.Build();
Log.Information("Starting application...");
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    await app.InitializeDb(); 
}
app.UseHangfireDashboard("/hangfire");

// 📌 Zamanlanmış Görev 
RecurringJob.AddOrUpdate<UserDeletionJob>(
    x => x.RunScheduledUserDeletion(),
    Cron.Daily(3, 00)
);
app.UseHangfireServer();

app.UseExceptionHandler("/Home/Error");
//app.UseHttpsRedirection(); // burasının kapanma sebebi mobilden gelen istekleri kabul etsin diye kapatıldı.
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "YummyApp API V1");
    c.RoutePrefix = string.Empty;
});

app.MapControllers();
app.MapFallbackToFile("/app/index.html");

app.Run();