using MediatR;
using yummyApp.Application.Services.Authencation;

namespace yummyApp.Application.Features.Users.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
    {
        readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.Password != request.PasswordConfirm)
            {
                return new RegisterCommandResponse
                {
                    Success = false,
                    Message = "Şifre ve şifre tekrarı eşleşmiyor!"
                };
            }
            RegisterCommandResponse response = await _authService.RegisterAsync(new()
           {
               Name = request.Name,
               Surname = request.Surname,
               Email = request.Email,
               Password = request.Password,
               PasswordConfirm = request.PasswordConfirm,

           });
            return new()
            {
                Message = response.Message,
                Success = response.Success
            };
        }
    }
}
