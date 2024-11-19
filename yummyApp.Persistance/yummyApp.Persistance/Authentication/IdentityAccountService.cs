using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using yummyApp.Application.Services.Account;
using yummyApp.Application.Services.Account.Models;
using yummyApp.Application.Services.Email;
using yummyApp.Domain.Constants;
using yummyApp.Domain.Identity;

namespace yummyApp.Persistance.Authentication
{
    public class IdentityAccountService:IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        public IdentityAccountService(UserManager<AppUser> userManager, RoleManager<UserRole> roleManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }
        public async Task<AuthenticationResponse?> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(e => e.Email == request.Email.Trim());
            if (user == null) return null;

            if (!request.IsExternalAuthentication)
            {
                var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, request.Password.Trim(), false);
                if (!checkPassword.Succeeded) return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.GivenName, user.Name ?? ""),
            new Claim(ClaimTypes.Surname, user.Surname ?? ""),
            new Claim(ClaimTypes.Email, request.Email)
        };

            if (roles.Any())
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                }
            }

            //await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
          //await _signInManager.SignInWithClaimsAsync(user, true, claims);
           await _signInManager.SignInAsync(user,true);
            

            return new AuthenticationResponse
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                UserName = user.UserName,
                Roles = roles.ToList(),
                FirstName = user.Name,
                LastName = user.Surname
            };
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id.ToString() == userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<AuthenticationResponse?> RegisterAsync(RegisterRequest request)
        {
            var activationCode = Convert.ToBase64String(AccountHelper.GenerateSalt());
            var user = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                Name = request.FirstName,
                Surname = request.LastName,
                EmailConfirmed = false,
                
            };

            if (request.IsExternalAuthentication)
            {
                user.EmailConfirmed = true;
            }

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var customerRole = new UserRole(Roles.User);
                var roles = new[] { customerRole.Name };
                var roleResult = await _userManager.AddToRolesAsync(user, roles);
                if (roleResult.Succeeded)
                {
                    if (!request.IsExternalAuthentication)
                    {
                        var siteUrl = "https://localhost:8000";
                        var body = @$"
                <h1>Yummy Application</h1>
                <p>User created! Please activate your account.</p>
                <p><a href=""{siteUrl}/app/account/activate?userId={user.Id}&code={activationCode}"">Activate</a></p>";
                        await _emailService.SendMailAsync(user.Email, "Yummy Application Register", body);
                    }

                    return new AuthenticationResponse
                    {
                        Id = user.Id.ToString(),
                        Email = user.Email,
                        UserName = user.UserName,
                        Roles = roles.ToList(),
                        FirstName = user.Name,
                        LastName = user.Surname
                    };
                }
            }
            return null;
        }

        public async Task<string?> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            return user?.UserName;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> ActivateUserAsync(string userId, string code)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id.ToString() == userId);
            if (user == null) return false;

            if (user.ActivationCode == code)
            {
                user.ActivationCode = "";
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                return true;
            }

            return false;
        }

        public async Task<bool> IsUserExist(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user != null;
        }
    }
}
