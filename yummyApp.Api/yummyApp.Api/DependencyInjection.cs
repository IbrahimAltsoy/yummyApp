using yummyApp.Application.Abstract.Common;
using yummyApp.Persistance.Authentication;
using yummyApp.Persistance.Services.Jwt;

namespace yummyApp.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(); 
            //services.AddHttpContextAccessor();
            services.AddScoped<IUser, CurrentUser>();
            
            //services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddScoped<JwtAccountService>();

            return services;
        }
    }
}
