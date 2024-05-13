using yummyApp.Application.Abstract.Common;
using yummyApp.Persistance.Authentication;

namespace yummyApp.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<IUser, CurrentUser>();

            return services;
        }
    }
}
