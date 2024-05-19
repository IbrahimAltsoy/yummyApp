using Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using yummyApp.Application.Abstract.Common;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Application.Repositories;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Application.Services.Account;
using yummyApp.Application.Services.Account.Email;
using yummyApp.Application.Services.Authencation;
using yummyApp.Application.Services.Email;
using yummyApp.Application.Services.Logger;
using yummyApp.Application.Services.Users;
using yummyApp.Application.UnitOfWork;
using yummyApp.Domain.Common;
using yummyApp.Domain.Identity;
using yummyApp.Persistance.Authentication;
using yummyApp.Persistance.Context;
using yummyApp.Persistance.Interceptors;
using yummyApp.Persistance.Repositories;
using yummyApp.Persistance.Services.Authencation;
using yummyApp.Persistance.Services.Email;
using yummyApp.Persistance.Services.Logging;
using yummyApp.Persistance.Services.Users;
using yummyApp.Persistance.UnitOfWorks;

namespace yummyApp.Persistance
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<YummyAppDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            });
            services.AddScoped<IYummyAppDbContext>(provider => provider.GetRequiredService<YummyAppDbContext>());
            services.AddSingleton(TimeProvider.System);
           
            services.AddIdentity<AppUser, UserRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<YummyAppDbContext>()
            .AddDefaultTokenProviders();
            services.AddScoped<IAccountService, IdentityAccountService>();
            //services.AddAuthorization();
            
            services.AddScoped<IBusinessRepository, BusinessRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IFriendShipRepository, FriendShipRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            services.AddScoped<IUserService, UserService>();            
            services.AddScoped<IAuthService, AuthService>();            
            services.AddScoped<IInternalAuthencation, AuthService>();
            services.AddScoped<IExternalAuthencation, AuthService>();

            services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));
            services.AddScoped<IEmailService, EmailService>();
            //services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMediaRepository, MediaRepository>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IAppLogger, LogManager>();
            return services;

        }
    }
}
