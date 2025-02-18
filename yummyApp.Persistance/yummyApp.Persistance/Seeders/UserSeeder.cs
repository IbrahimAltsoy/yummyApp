using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Domain.Enums;
using yummyApp.Domain.Identity;
using yummyApp.Infrastructure.Common;

namespace yummyApp.Persistance.Seeders
{
    public class UserSeeder : ISeeder
    {
        public async Task Seed(IYummyAppDbContext context)
        {
            if (context.AppUsers.Any()) return;
            var hasher = new PasswordHasher<AppUser>();
            var adminRoleId = new Guid("12345678-1234-1234-1234-123456789012");

            
            var adminUser = new AppUser()
            {
                UserName = "admin",
                Name = "string",
                Surname = "Değerli",
                ActivationCode = "",
                NormalizedUserName = "ADMIN",
                Email = "admin@yummyapp.com",
                NormalizedEmail = "ADMIN@YUMMYAPP.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin123!"),  // 📌 Şifreyi hashliyoruz
                SecurityStamp = "E51C5B6B-9E85-42AB-8A8C-B65A3D74A12D",
                CreatedAt = new DateTime(2024, 2, 18, 12, 0, 0)  // 📌 Sabit tarih         


            };            
            await context.AppUsers.AddAsync(adminUser);

            await context.SaveChangesAsync();
        }

    }
}
