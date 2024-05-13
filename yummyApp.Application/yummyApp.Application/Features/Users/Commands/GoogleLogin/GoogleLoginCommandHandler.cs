using MediatR;
using yummyApp.Application.Services.Authencation;
using yummyApp.Application.Tokens;

namespace yummyApp.Application.Features.Users.Commands.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly IAuthService _authService;
        readonly ITokenHandler _tokenHandler;

        public GoogleLoginCommandHandler(IAuthService authService, ITokenHandler tokenHandler)
        {
            _authService = authService;
            _tokenHandler = tokenHandler;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.GoogleLoginAsync(request.IdToken, 15 * 60);
            return new()
            {
                Token = token

            };
        }
    }
}
