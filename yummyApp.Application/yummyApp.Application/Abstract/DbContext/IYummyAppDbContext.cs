using Microsoft.EntityFrameworkCore;
using yummyApp.Domain.Entities;

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
        DbSet<User> Users { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
