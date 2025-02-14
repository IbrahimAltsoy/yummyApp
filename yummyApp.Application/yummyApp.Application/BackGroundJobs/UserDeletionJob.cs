using Hangfire;
using Microsoft.EntityFrameworkCore;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Application.Services.Users;

namespace yummyApp.Application.BackGroundJobs
{
    public class UserDeletionJob
    {
        private readonly IYummyAppDbContext _dbContext;
        private readonly IUserService _userService;

        public UserDeletionJob(IYummyAppDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }
        [AutomaticRetry(Attempts = 3)] // 📌 Eğer job başarısız olursa 3 kez tekrar dener
        public async Task RunScheduledUserDeletion()
        {
            var usersToDelete = await _dbContext.AppUsers
                .IgnoreQueryFilters()
                .Where(u => u.DeletedAt < DateTime.UtcNow.AddDays(-30)) // 📌 30 gün geçmiş kullanıcıları bul
                .ToListAsync();

            foreach (var user in usersToDelete)
            {
                await _userService.HardDeleteUserAsync(user.Id); // 📌 Kullanıcıyı kalıcı olarak sil
            }
        }
    }
}
