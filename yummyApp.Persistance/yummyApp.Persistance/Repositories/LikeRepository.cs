using yummyApp.Application.Repositories.Repository;
using yummyApp.Application.Repositories;
using yummyApp.Domain.Entities;
using yummyApp.Persistance.Context;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using yummyApp.Application.Features.Likes.Queries.GetAll;

namespace yummyApp.Persistance.Repositories
{
    public class LikeRepository : EfRepositoryBase<Like, Guid, YummyAppDbContext>, ILikeRepository
    {
        public LikeRepository(YummyAppDbContext context) : base(context)
        {
            
        }

        public async Task<List<User>> UsersWhoLikedAsync(Guid postId)
        {
            var likes = await Context.Likes
            .Where(l => l.PostID == postId && l.DeletedAt == null)
            .Select(l => l.UserID)
            .ToListAsync();
            var usersWhoLiked = await Context.Users
                .Where(u => likes.Contains(u.Id))
                .Select(u => new User
                {
                    Name = u.Name,
                    Surname = u.Surname,
                })
                .ToListAsync();
            return usersWhoLiked;
        }
    }
}
