using Microsoft.EntityFrameworkCore;
using yummyApp.Domain.Entities;
using yummyApp.Domain.Identity;

namespace yummyApp.Application.Abstract.DbContext
{
    public interface IYummyAppDbContext
    {
        DbSet<Business> Businesses { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Friendship> Friendships { get; set; }
        DbSet<Like> Likes { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<BusinessLocation> BusinessLocations { get; set; }
        DbSet<PostLocation> PostLocations { get; set; }
        DbSet<AppUser> AppUsers { get; set; }
        DbSet<UserFeedback> UserFeedbacks { get; set; }
        //DbSet<LogEntry> LogEntries { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
