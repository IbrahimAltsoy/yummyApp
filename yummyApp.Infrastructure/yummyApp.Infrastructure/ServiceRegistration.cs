using Microsoft.Extensions.DependencyInjection;
using yummyApp.Application.Tokens;
using yummyApp.Infrastructure.Common;
using yummyApp.Infrastructure.Storage.Google;
using yummyApp.Infrastructure.Token;
using static yummyApp.Domain.Enums.PlaceCategories;

namespace yummyApp.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            //serviceCollection.AddScoped<IFileService, FileService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            serviceCollection.AddSingleton<IGoogleCloudStorageService, GoogleCloudStorageService>();


        }
    }
}
