using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.Design;
using yummyApp.Application.Services.Authencation;
using yummyApp.Domain.Identity;

namespace yummyApp.Application.Features.Users.Commands.PasswordReset
{
    public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommandRequest, PasswordResetCommandResponse>
    {
        readonly IAuthService _authService;
        readonly UserManager<AppUser> _userManager;

        public PasswordResetCommandHandler(IAuthService authService, UserManager<AppUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        public async Task<PasswordResetCommandResponse> Handle(PasswordResetCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.FindByEmailAsync(request.Email!);
            if (user == null)
            {
                return new() { Message = "Kullanıcı bulunamadı", Success = false };
            }
            else {

                await _authService.ResetPasswordAsync(request.Email!);
                return new() { Message = "Şifre için gelen emaili kontrol ediniz.", Success = true };
            }

        }
    }
}
