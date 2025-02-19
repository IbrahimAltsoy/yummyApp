using yummyApp.Api.Infrastructure;
using yummyApp.Application.Abstract.Common;
using yummyApp.Application.Services.GoogleApi;
using yummyApp.Persistance.Authentication;
using yummyApp.Persistance.Services.GoogleApi;
using yummyApp.Persistance.Services.Jwt;

namespace yummyApp.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddControllers();
            //services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen(); 
            services.AddScoped<IUser, CurrentUser>();
            //services.AddExceptionHandler<GlobalExceptionHandler>();

            
            services.AddScoped<JwtAccountService>();

            services.AddScoped<IGooglePlacesService, GooglePlacesService>();

            return services;
        }
    }
}
