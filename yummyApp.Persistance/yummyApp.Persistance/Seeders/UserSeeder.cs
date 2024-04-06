using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Domain.Entities;
using yummyApp.Domain.Enums;
using yummyApp.Infrastructure.Common;
using yummyApp.Persistance.Context;

namespace yummyApp.Persistance.Seeders
{
    public class UserSeeder : ISeeder
    {
        public async Task Seed(IYummyAppDbContext context)
        {
            if (context.Users.Any()) return;

            var adminUser = new User()
            {
                Username = "admin",
                Password ="Yummy123." ,/*AccountHelper.HashCreate("123qwe"),*/
                Email = "admin@localhost",
                ConfirmPassword = "Yummy123.",
                //Roles = "Administrator",
                Gender = Gender.Male,
                Name = "Admin",
                Surname= "Admin",
                IsActive = true,
                ProfilePicture="string",
                PrivacySettings= true,
                NotificationPreferences=true,

            };
            await context.Users.AddAsync(adminUser);

            var trSet = new Bogus.DataSets.Name(locale: "tr");
            var faker = new Faker<User>()
                .RuleFor(u => u.Name, (f, u) => f.Name.FirstName())
                .RuleFor(s => s.Surname, (f, s) => f.Name.LastName())
                .RuleFor(u => u.Username, (f, u) => f.Internet.UserName(u.Name, u.Surname))
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name, u.Surname))
                .RuleFor(u => u.Password, "Yummy123." /*AccountHelper.HashCreate("123qwe")*/)
                .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
                .RuleFor(u => u.ProfilePicture, f => f.Image.ToString())
                .RuleFor(u => u.IsActive, f => f.Random.Bool())
                .RuleFor(u => u.NotificationPreferences, f => f.Random.Bool())
                .RuleFor(u => u.PrivacySettings, f => f.Random.Bool());
            var list = faker.Generate(100);
            await context.Users.AddRangeAsync(list);

            await context.SaveChangesAsync();
        }

    }
}
