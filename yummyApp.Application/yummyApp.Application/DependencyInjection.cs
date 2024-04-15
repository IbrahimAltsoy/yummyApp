using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using yummyApp.Application.Behaviors;
using yummyApp.Application.Repositories;

namespace yummyApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
                           
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatR(media =>
            {
                media.RegisterServicesFromAssembly(assembly);
                media.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
                media.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                media.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
            });

            return services;
        }
    }
}
