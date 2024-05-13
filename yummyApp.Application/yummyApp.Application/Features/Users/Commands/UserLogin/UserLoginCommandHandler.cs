using MediatR;
using yummyApp.Application.Services.Authencation;

namespace yummyApp.Application.Features.Users.Commands.UserLogin
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommandRequest, UserLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public UserLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<UserLoginCommandResponse> Handle(UserLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var accessToken = await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 15 * 60);
            return new UserLoginCommandResponse()
            {
                AccessToken = accessToken,
                Message = "Giriş Başarılı",
                UserName = request.UsernameOrEmail,
            };
        }
    }
}
