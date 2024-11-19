using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using yummyApp.Application.Abstract.Common;
using yummyApp.Application.Exceptions.AuthExceptions;
using yummyApp.Application.Services.Account.Models;
using yummyApp.Domain.Identity;

namespace yummyApp.Persistance.Services.Jwt
{
    public class JwtAccountService
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly IHttpContextAccessor _contextAccessor;
        

        public JwtAccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
            
        }


        public async Task<List<Claim>?> AuthenticateAsync(AuthenticationRequest request)
        {
            
            var user = await _userManager.Users.FirstOrDefaultAsync(e => e.Email == request.Email);
            if (user.IsActive == false) return null;
            if (user == null) return null;           
            var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, request.Password.Trim(), false);
            if (!checkPassword.Succeeded) return null;            
            var claims = await GetUserClaims(user);
            var identity = new ClaimsIdentity(claims, "jwt");
            var principal = new ClaimsPrincipal(identity);
            var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password.Trim(), false, lockoutOnFailure: false);
            if (signInResult.Succeeded)
            {
                var x = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return claims;
            }
            else
            {
                return null;
            }

            
        }

        private async Task<List<Claim>?> GetUserClaims(AppUser? user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.GivenName, user.Name ?? ""),
            new Claim(ClaimTypes.Surname, user.Surname ?? ""),
            new Claim(ClaimTypes.Email, user.Email),
            
        };           
            if (roles.Any())
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                }
            }

            return claims;
        }

        public async Task<List<Claim>?> AuthenticateByUserIdAsync(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(e => e.Id.ToString() == id);
            if (user == null) return null;

            return await GetUserClaims(user);
        }

        public async Task<RefreshTokenResponse?> GetUserByRefreshToken(string refreshToken)
        {
            var user = await _userManager.Users.Where(e => e.RefreshToken == refreshToken).FirstOrDefaultAsync();
            if (user == null) return null;

            return new RefreshTokenResponse
            {
                Id = user.Id.ToString(),
                RefreshToken = user.RefreshToken
            };
        }

        public async Task<bool> UpdateRefreshToken(string userId, string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(e => e.Id.ToString() == userId);
            if (user == null) return false;

            user.RefreshToken = refreshToken;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}
