using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Domain.Common;
using yummyApp.Domain.Entities;
using yummyApp.Domain.Identity;
using yummyApp.Application.Services.Authencation;
using yummyApp.Persistance.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Diagnostics;


public class YummyAppDbContext : IdentityDbContext<AppUser, UserRole, Guid>, IYummyAppDbContext
{
    public YummyAppDbContext(DbContextOptions<YummyAppDbContext> options) : base(options)
    {
    }
    public YummyAppDbContext() { }

    public DbSet<Business> Businesses { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Friendship> Friendships { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Media> Medias { get; set; }
    public DbSet<BusinessLocation> BusinessLocations { get; set; }
    public DbSet<PostLocation> PostLocations { get; set; }
    //public DbSet<LogEntry> LogEntries { get; set; }

    public DbSet<AppUser> AppUsers { get; set; }

   // public DbSet<User> Users { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Friendship>()
         .HasOne(f => f.Follower)
         .WithMany()
         .HasForeignKey(f => f.FollowerID)
         .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Friendship>()
            .HasOne(f => f.Followee)
            .WithMany()
            .HasForeignKey(f => f.FolloweeID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AppUser>()
       .HasMany(u => u.ReceivedMessages)
       .WithOne(m => m.Receiver)
       .HasForeignKey(m => m.ReceiverId)
       .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BusinessLocation>()
    .HasOne(bl => bl.Business)
    .WithMany()
    .HasForeignKey(bl => bl.Id)
    .OnDelete(DeleteBehavior.Restrict)  // Silme davranışını belirle
    .IsRequired(false);  // Burası önemli! Optional yapıyoruz

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Business)
            .WithMany()
            .HasForeignKey(p => p.BusinessId)
            .OnDelete(DeleteBehavior.Restrict)  // Silme davranışını belirle
            .IsRequired(false);  // Burası önemli! Optional yapıyoruz

        // Foreign key ilişkileri için silme davranışını sınırlamak
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        // Fluent API konfigürasyonlarını uygulamak

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }


}


