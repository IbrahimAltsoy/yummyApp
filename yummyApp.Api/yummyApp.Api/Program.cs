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
using yummyApp.Api.Filters;
using Serilog.Events;
using yummyApp.Api.Infrastructure;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Persistance.Seeders;
using Microsoft.AspNetCore.Diagnostics;



var builder = WebApplication.CreateBuilder(args);
#region Serilog Configuration
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()    
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
        {
            TableName = "LogEntries",
            AutoCreateSqlTable = false // Eğer tablo yoksa otomatik oluştur
        })
    .CreateLogger();
#endregion

builder.WebHost.UseUrls("http://0.0.0.0:7009"); // burası mobilden giriş yapabilmek için eklendi.
builder.Host.UseSerilog();


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

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
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT Authorization header kullanarak giriş yapın. Örn: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat ="JWT"
        
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


var app = builder.Build();
app.UseExceptionHandler(_ => { });


Log.Information("Starting application...");
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    await app.InitializeDb();
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<IYummyAppDbContext>();
        var userSeeder = new UserSeeder();
        await userSeeder.Seed(dbContext);
    }
}
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangfireAuthorizationFilter() }
});
// 📌 Zamanlanmış Görev 
RecurringJob.AddOrUpdate<UserDeletionJob>(
    x => x.RunScheduledUserDeletion(),
    Cron.Daily(3, 00)
);
app.UseHangfireServer();


//app.UseHttpsRedirection(); // burasının kapanma sebebi mobilden gelen istekleri kabul etsin diye kapatıldı.
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
    c.RoutePrefix = string.Empty;
});

app.MapControllers();
app.MapFallbackToFile("/app/index.html");
app.Run();