using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Application.UnitOfWork;
using yummyApp.Infrastructure.Identity;
using yummyApp.Persistance.Context;
using yummyApp.Persistance.Interceptors;
using yummyApp.Persistance.Repositories;
using yummyApp.Persistance.UnitOfWorks;

namespace yummyApp.Persistance
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            //services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
            services.AddScoped<IBusinessRepository, BusinessRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IFriendShipRepository, FriendShipRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMediaRepository, MediaRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
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
