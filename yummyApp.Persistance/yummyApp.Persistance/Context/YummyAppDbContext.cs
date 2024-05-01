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
using yummyApp.Infrastructure.Identity;

public class YummyAppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IYummyAppDbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbSet<Business> Businesses { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Friendship> Friendships { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Media> Medias { get; set; }
    public DbSet<BusinessLocation> BusinessLocations { get; set; }
    public DbSet<PostLocation> PostLocations { get; set; }
    public DbSet<User> Users { get; set; }

    public YummyAppDbContext(DbContextOptions<YummyAppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

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
        modelBuilder.Entity<User>()
       .HasMany(u => u.ReceivedMessages)
       .WithOne(m => m.Receiver)
       .HasForeignKey(m => m.ReceiverId)
       .OnDelete(DeleteBehavior.Restrict);
        // Foreign key ilişkileri için silme davranışını sınırlayın
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        // Fluent API konfigürasyonlarını uygulamak
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        // Business ve BusinessLocation arasındaki ilişkiyi belirle
        //modelBuilder.Entity<Business>()
        //.HasOne(b => b.Location)
        //.WithOne(l => l.Business)
        //.HasForeignKey<BusinessLocation>(l => l.Id);

       // modelBuilder.Entity<Post>()
       //.HasOne(p => p.PostLocation)
       //.WithOne(pl => pl.Post)
       //.HasForeignKey<Post>(p => p.PostLocationId)
       //.IsRequired(false);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditInformation();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetAuditInformation()
    {
        var entries = ChangeTracker.Entries<IAuditableEntity>().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "unknown";

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                //entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                entry.Property("CreatedBy").CurrentValue = userId;
            }
            else if (entry.State == EntityState.Modified)
            {
                
                if (entry.Property("DeletedAt").IsModified)
                {
                    entry.Property("DeletedBy").CurrentValue = userId;
                }
                else 
                {
                    entry.Property("LastModifiedBy").CurrentValue = userId;
                }
                
            }
            
        }

        
    }
}

public class CurrentUserValueGenerator : ValueGenerator<string>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserValueGenerator(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public override bool GeneratesTemporaryValues => false;

    public override string Next(EntityEntry entry)
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "unknown";
        return userId;
    }
}
