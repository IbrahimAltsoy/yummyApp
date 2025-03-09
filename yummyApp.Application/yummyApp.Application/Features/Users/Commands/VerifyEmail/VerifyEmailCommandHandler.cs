using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Web;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Application.Services.Users;
using yummyApp.Domain.Identity;

namespace yummyApp.Application.Features.Users.Commands.VerifyEmail
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommandRequest, VerifyEmailCommandResponse>
    {
        readonly UserManager<AppUser> _userManager;
        readonly IUserService _userService;

        public VerifyEmailCommandHandler(UserManager<AppUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;

        }

        public async Task<VerifyEmailCommandResponse> Handle(VerifyEmailCommandRequest request, CancellationToken cancellationToken)
        {

            // **Düzgün decoding yapıyoruz**
            string decodedActivationCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.ActivationCode));

            // Kullanıcıyı veritabanında bul
            AppUser? user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || user.ActivationCode != decodedActivationCode)
            {
                return new VerifyEmailCommandResponse()
                {
                    Succeeded = false,
                    Message = "Doğrulama başarısız! Lütfen kontrol ediniz!"
                };
            }

            // Kullanıcıyı aktif hale getir
            user.IsActive = true;
            user.ActivationCode = "";
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);

            // Kullanıcıya rol ekle
            await _userManager.AddToRoleAsync(user, "User");

            return new VerifyEmailCommandResponse()
            {
                Succeeded = true,
                Message = "Doğrulama başarıyla gerçekleşti."
            };
        }
    }
}
