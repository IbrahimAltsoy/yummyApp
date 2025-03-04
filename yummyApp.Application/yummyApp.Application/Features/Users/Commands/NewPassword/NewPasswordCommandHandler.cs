using MediatR;
using System.Text;
using yummyApp.Application.Services.Authencation;

namespace yummyApp.Application.Features.Users.Commands.NewPassword
{
    public class NewPasswordCommandHandler : IRequestHandler<NewPasswordCommandRequest, NewPasswordCommandResponse>
    {
        readonly IAuthService _authService;

        public NewPasswordCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<NewPasswordCommandResponse> Handle(NewPasswordCommandRequest request, CancellationToken cancellationToken)
        {            
            var result = await _authService.ResetPasswordWithTokenAsync(request.UserId, request.Token!, request.NewPassword!);
            if (result) return new() { Message = "Yeni şifre başarılı bir şekilde oluşturuldu.", Success = true };
            return new() { Message = "Şifre oluşturuken hata oluştu.", Success = false };
        }
    }
}
