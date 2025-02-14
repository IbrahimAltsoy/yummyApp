using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using yummyApp.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using yummyApp.Persistance.Seeders;


namespace yummyApp.Persistance.Context
{
    public static class DbInitExtensions
    {
        public static async Task InitializeDb(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<YummyAppDbContext>();

        }
    }
}
