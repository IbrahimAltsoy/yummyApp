using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using yummyApp.Application.Exceptions.AuthExceptions;
using yummyApp.Application.Rules;
using yummyApp.Application.Services.Email;
using yummyApp.Domain.Identity;

namespace yummyApp.Application.Features.Users.Rules
{
    public class AuthBusinessRules : BaseBusinessRules
    {
        readonly UserManager<AppUser> _userManager;
        readonly IEmailService _emailService;
        readonly string _baseUrl;


        public AuthBusinessRules(UserManager<AppUser> userManager, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _emailService = emailService;
            _baseUrl = configuration["ApplicationSettings:MobileApplication"]!;
        }
        public Task UserShouldBeExists(AppUser? user)
        {
            if (user == null)
                throw new NotFoundUserExceptions();
            return Task.CompletedTask;
        }
        public async Task UserEmailShouldBeNotExists(string email)
        {
            AppUser? user = await _userManager.FindByEmailAsync(email);
            if (user != null)
                throw new UserAlreadyExistsException(email);
        }
        public async Task UserEmailVerifyCheck(AppUser? user)
        {
            if (!user!.EmailConfirmed)
            {
                string? activeCode = await _emailService.CreateEmailActivationKey();
                user.ActivationCode = activeCode;
                await _userManager.UpdateAsync(user);
                string encodedEmail = Uri.EscapeDataString(user.Email!);
                string encodedActivationCode = Uri.EscapeDataString(activeCode);
                string activationLink = $"{_baseUrl}/api/Auth/VerifyEmail?Email={encodedEmail}&ActivationCode={encodedActivationCode}";
                await _emailService.SendMailAsync(
                    user.Email,
                    "Aktivasyon Kodu",
                    $"Kaydınızı doğrulamak için aşağıdaki bağlantıya tıklayınız: <a href='{activationLink}'>Aktivasyon Linki</a>");
                throw new UserEmailVerifyCheckException();

            }
        }
      
    } 
}
