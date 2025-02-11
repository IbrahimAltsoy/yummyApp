using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using yummyApp.Application.Exceptions.AuthExceptions;
using yummyApp.Application.Rules;
using yummyApp.Application.Services.Email;
using yummyApp.Domain.Identity;

namespace yummyApp.Application.Features.Users.Rules
{
    // Business Rule eklenecek onun akabinde Email servisinde belirli kodlar entegre edilecek(CreateEmailActivationKey)(Emailde oluşan linki oluştur ve congigration den atamasını yaptır), UserEmailVerifyCheck bu metod düzeltilecek ve testleri yapılacaktır (UserEmailVerifyCheck te oluşan linki değiştirmen lazım ve configration dan atamsını yap).
    public class AuthBusinessRules : BaseBusinessRules
    {
        readonly UserManager<AppUser> _userManager;
        readonly IEmailService _emailService;


        public AuthBusinessRules(UserManager<AppUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }
        public Task UserShouldBeExists(AppUser? user)
        {
            if (user == null)
                //throw new BusinessException(AuthMessages.UserDontExists);
                throw new NotFoundUserExceptions();
            return Task.CompletedTask;
        }
        public async Task UserEmailShouldBeNotExists(string email)
        {
            AppUser? user = await _userManager.FindByEmailAsync(email);
            if (user != null)
                // throw new BusinessException(AuthMessages.UserMailAlreadyExists);
                throw new UserAlreadyExistsException(email);
        }
        public async Task UserEmailVerifyCheck(AppUser? user)
        {
            if (!user.EmailConfirmed)
            {
                string? activeCode = await _emailService.CreateEmailActivationKey();
                user.ActivationCode = activeCode;
                await _userManager.UpdateAsync(user);
                string encodedEmail = Uri.EscapeDataString(user.Email);
                string encodedActivationCode = Uri.EscapeDataString(activeCode);
                string activationLink = $"https://localhost:5218/api/Auth/VerifyEmail?Email={encodedEmail}&ActivationCode={encodedActivationCode}";
                await _emailService.SendMailAsync(
                    user.Email,
                    "Aktivasyon Kodu",
                    $"Kaydınızı doğrulamak için aşağıdaki bağlantıya tıklayınız: <a href='{activationLink}'>Aktivasyon Linki</a>");
                //throw new BusinessException(AuthMessages.UserEmailHasNotBeenVerified);
                throw new UserEmailVerifyCheckException();

            }
        }
        //public async Task UserPhoneNumberShouldBeNotExists(string phoneNumber)
        //{
        //    var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        //    if (user != null)
        //        throw new BusinessException(AuthMessages.UserPhoneNumberAlreadyRegistered);
        //}
    } 
}
