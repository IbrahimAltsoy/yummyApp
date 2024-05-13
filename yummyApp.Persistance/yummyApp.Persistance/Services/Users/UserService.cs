using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yummyApp.Application.Abstract.Common;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Application.Dtos.Users;
using yummyApp.Application.Exceptions.AuthExceptions;
using yummyApp.Application.Features.Users.Commands.Create;
using yummyApp.Application.Helpers;
using yummyApp.Application.Paging;
using yummyApp.Application.Services.Users;
using yummyApp.Domain.Identity;

namespace yummyApp.Persistance.Services.Users
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly IMapper _mapper;
        readonly IYummyAppDbContext _dbContext;

        public UserService(UserManager<AppUser> userManager, IMapper mapper, IYummyAppDbContext dbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<CreateUserCommandResponse> CreateUserAsync(UserCreateDto userCreate)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                //Id = new(),
                Name = userCreate.Name,
                Surname = userCreate.Surname,
                UserName = userCreate.UserName,
                Gender = userCreate.Gender,
                Birthday = userCreate.Birthday,
                IsActive = userCreate.IsActive,
                PhoneNumber = userCreate.PhoneNumber,
                Email = userCreate.Email

            }, userCreate.Password);
            CreateUserCommandResponse response = new() { Succeeded = result.Succeeded };
            if (result.Succeeded)
                response.Message = "Yeni kullanıcı kaydı başarılı bir şekilde gerçekleşti.";
            else
            {
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Description} - {error.Code}<br>";
                }
            }
            return response;
        }

        public async Task<List<UserReadDto>> GetUserAllAsync()
        {
            List<AppUser> users =  _dbContext.AppUsers.ToList();
            List<UserReadDto> result = new List<UserReadDto>();
            foreach (var user in users.ToList())
            {
                result.Add(_mapper.Map<UserReadDto>(user));
            }
            return result;
        }

        public async Task<UserReadDto> GetUserByIdAsync(Guid userId)
        {
            var result = await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Id == userId);
            var map = _mapper.Map<UserReadDto>(result);
            return map;
        }

        public async Task UpdatePasswordAsync(Guid userId, string resetToken, string newPassword)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);
                else new PasswordChangeFailledException();
            }
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenTime)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenTime);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserExceptions();
        }

        public async Task<IdentityResult> UpdateUserAsync(UserUpdateDto userCreate)
        {
            AppUser? user = await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Id == userCreate.Id);

            user.Name = userCreate.Name;
            user.Surname = userCreate.Surname;
            user.UserName = userCreate.UserName;
            user.Email = userCreate.Email;
            user.PhoneNumber = userCreate.PhoneNumber;
            user.Birthday = userCreate.Birthday;
            user.IsActive = userCreate.IsActive;
            //await _commandRepository.Update(user);
            //user.PasswordHash = userCreate.Password;
            var result = await _userManager.UpdateAsync(user);
            return result;
        }
    }
}
