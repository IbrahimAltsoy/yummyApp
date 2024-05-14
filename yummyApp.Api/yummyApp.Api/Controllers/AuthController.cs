using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using yummyApp.Application.Features.Users.Commands.GoogleLogin;
using yummyApp.Application.Features.Users.Commands.PasswordReset;
using yummyApp.Application.Features.Users.Commands.UserLogin;
using yummyApp.Application.Features.Users.Commands.VerifyResetToken;
using yummyApp.Application.Services.Account.Models;
using yummyApp.Persistance.Authentication;
using yummyApp.Persistance.Services.Jwt;

namespace yummyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
         readonly JwtAccountService _accountService;
         readonly IConfiguration _configuration;

        public AuthController(JwtAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticationRequest request)
        {
            var authenticatedUserClaims = await _accountService.AuthenticateAsync(request);
            if (authenticatedUserClaims == null) return Unauthorized();

            //var expireInMinute = Convert.ToDouble(_configuration[]);
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                claims: authenticatedUserClaims,
                expires: DateTime.UtcNow.AddMinutes(50),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            var refreshToken = AccountHelper.GenerateSalt();

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = TimeSpan.FromMinutes(50).TotalSeconds,
            });
        }
        //[HttpPost("google-login")]
        //public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
        //{
        //    GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
        //    return Ok(response);
        //}
        //[HttpPost("password-reset")]
        //public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest request)
        //{
        //    PasswordResetCommandResponse response = await _mediator.Send(request);
        //    return Ok(response);
        //}
        //[HttpPost("verify-reset-token")]
        //public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest request)
        //{
        //    VerifyResetTokenCommandResponse response = await _mediator.Send(request);
        //    return Ok(response);
        //}

    }
}
