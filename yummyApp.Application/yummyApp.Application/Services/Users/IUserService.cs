using Microsoft.AspNetCore.Identity;
using yummyApp.Application.Abstract.Common;
using yummyApp.Application.Dtos.Users;
using yummyApp.Application.Features.Users.Commands.Create;
using yummyApp.Domain.Identity;

namespace yummyApp.Application.Services.Users
{
    public interface IUserService
    {
        Task<List<UserReadDto>> GetUserAllAsync();
        Task<CreateUserCommandResponse> CreateUserAsync(UserCreateDto userCreate);
        Task<IdentityResult> UpdateUserAsync(UserUpdateDto userCreate);
        Task<UserReadDto> GetUserByIdAsync(Guid userId);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenTime);
        Task UpdatePasswordAsync(Guid userId, string resetToken, string newPassword);
        Task<bool> SoftDeleteUserAsync(Guid userId);
        Task<bool> HardDeleteUserAsync(Guid userId);
        //Task<string> GetUserRoleAsync(UserReadDto user);
    }
}
