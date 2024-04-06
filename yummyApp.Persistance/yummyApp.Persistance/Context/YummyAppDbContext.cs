using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Domain.Entities;
using yummyApp.Infrastructure.Identity;

namespace yummyApp.Persistance.Context
{
    public class YummyAppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IYummyAppDbContext
    {
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }


        public YummyAppDbContext(DbContextOptions<YummyAppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
