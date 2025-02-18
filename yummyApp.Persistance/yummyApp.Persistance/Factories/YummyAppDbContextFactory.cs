using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace yummyApp.Persistance.Factories
{
    public class YummyAppDbContextFactory:IDesignTimeDbContextFactory<YummyAppDbContext>
    {
        public YummyAppDbContext CreateDbContext(string[] args)
        {
            // API Projesinin Root Yolunu Belirleyelim
            string basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../../yummyApp.Api/yummyApp.Api/"));

            Console.WriteLine($"[DEBUG] AppSettings Path: {basePath}"); // Debug için log ekledik

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<YummyAppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new YummyAppDbContext(builder.Options);
        }
    }
}
