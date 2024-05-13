using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using yummyApp.Application.Features.Users.Commands.GoogleLogin;
using yummyApp.Application.Features.Users.Commands.PasswordReset;
using yummyApp.Application.Features.Users.Commands.UserLogin;
using yummyApp.Application.Features.Users.Commands.VerifyResetToken;

namespace yummyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserLoginCommandRequest request)
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                // Kullanıcı bilgilerine erişim sağla burada eklemeler yapıldı 
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

                // Diğer işlemleri gerçekleştir
                UserLoginCommandResponse response = await _mediator.Send(request);
                return Ok(response);
            }
            else
            {
                // Kullanıcı kimliği doğrulanamadı veya HttpContext nesnesi yok
                // Hata işleme veya uygun bir cevap döndürme
                return Unauthorized();
            }
        }
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
            return Ok(response);
        }
        [HttpPost("password-reset")]
        public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest request)
        {
            PasswordResetCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("verify-reset-token")]
        public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest request)
        {
            VerifyResetTokenCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}
