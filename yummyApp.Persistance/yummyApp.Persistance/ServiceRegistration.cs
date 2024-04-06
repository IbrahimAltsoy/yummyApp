using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using yummyApp.Application.Abstract.DbContext;
using yummyApp.Infrastructure.Identity;
using yummyApp.Persistance.Context;
using yummyApp.Persistance.Interceptors;

namespace yummyApp.Persistance
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            //services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<YummyAppDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IYummyAppDbContext>(provider => provider.GetRequiredService<YummyAppDbContext>());

            services.AddSingleton(TimeProvider.System);

           

            ////services.AddDatabaseDeveloperPageExceptionFilter();
            //services
            //    .AddDefaultIdentity<ApplicationUser>(options =>
            //    {
            //        options.SignIn.RequireConfirmedAccount = false;
            //        options.SignIn.RequireConfirmedEmail = true;
            //        options.User.RequireUniqueEmail = false;
            //        options.Password.RequiredLength = 10;
            //    })
            //    .AddRoles<ApplicationRole>()
            //    .AddEntityFrameworkStores<YummyAppDbContext>();

           

          
            //services.AddScoped<IdentitySeeder>();
            //services.AddAuthorization();

            // Generic Repository, Unit of Work Pattern
            //services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

           
            //services.AddScoped<INotificationRepository, NotificationRepository>();

            
            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = configuration["Redis"];
            //    options.InstanceName = "RedisOrnek";
            //});
            //services.AddScoped<IAppCache, RedisCache>();

            //// Logging
            //services.AddSingleton<IAppLogger, LogManager>();

            return services;

        }
    }
}
