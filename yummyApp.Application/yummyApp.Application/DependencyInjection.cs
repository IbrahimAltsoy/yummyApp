using Microsoft.Extensions.DependencyInjection;
using yummyApp.Application.Repositories;

namespace yummyApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
           
            return services;
        }
    }
}
