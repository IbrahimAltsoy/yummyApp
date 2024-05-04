using Microsoft.EntityFrameworkCore;
using yummyApp.Domain.Entities;
using yummyApp.Domain.Identity;

namespace yummyApp.Application.Abstract.DbContext
{
    public interface IYummyAppDbContext
    {
        DbSet<Business> Businesses { get; }
        DbSet<Comment> Comments { get; }
        DbSet<Friendship> Friendships { get; }
        DbSet<Like> Likes { get; }
        DbSet<Post> Posts { get; }
        DbSet<Tag> Tags { get; }
        DbSet<BusinessLocation> BusinessLocations { get; }
        DbSet<PostLocation> PostLocations { get; }
        DbSet<AppUser> AppUsers { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
