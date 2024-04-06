using Microsoft.Extensions.DependencyInjection;
using yummyApp.Infrastructure.Common;

namespace yummyApp.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            //serviceCollection.AddScoped<IFileService, FileService>();
            
        }
    }
}
