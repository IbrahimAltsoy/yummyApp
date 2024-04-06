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

            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            // Migration yapısını kullandığımız durumda proje çalıştığında Migration işlemini yapar.
            //context.Database.Migrate();

            // IdentitySeeder DI yapısıyla çalıştığı için bu şekilde kullandık.
            ////var identitySeeder = scope.ServiceProvider.GetRequiredService<IdentitySeeder>();
            ////await identitySeeder.Seed(context);

            // Users tablosu diğer seeder'larda kullanıldığı için bunu öncelikli yapmasını istedik.
            ////await new UserSeeder().Seed(context);
            //await new UserAddressSeeder().Seed(context);

            // IdentitySeeder DI yapısıyla çalıştığı için bu şekilde kullandık.
            ////var identitySeeder = scope.ServiceProvider.GetRequiredService<IdentitySeeder>();
            ////await identitySeeder.Seed(context);

            //await new UserSeeder().Seed(context);


            // ISeeder arayüzünden türeyen tüm Seeder'ları çalıştırmayı sağlar.
            //await ApplyAllSeederFromAssembly(context);
        }

        //private static async Task ApplyAllSeederFromAssembly(YummyAppDbContext context)
        //{
        //    var seederType = typeof(ISeeder);
        //    var seeders = Assembly.GetExecutingAssembly().GetTypes()
        //        .Where(s => seederType.IsAssignableFrom(s) && s != seederType)
        //        .ToList();
        //    foreach (var type in seeders)
        //    {
        //        try
        //        {
        //            var seeder = Activator.CreateInstance(type) as ISeeder;
        //            await seeder?.Seed(context);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex);
        //        }
        //    }
        //}
    }
}
