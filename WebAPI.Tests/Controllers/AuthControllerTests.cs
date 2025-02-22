using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using yummyApp.Application.Features.Users.Commands.NewPassword;
using yummyApp.Application.Features.Users.Commands.PasswordReset;
using yummyApp.Application.Features.Users.Commands.Register;
using yummyApp.Application.Features.Users.Commands.UserLogin;
using yummyApp.Application.Features.Users.Commands.VerifyEmail;
using FluentAssertions;
using Xunit;
using Moq;
using yummyApp.Api.Controllers;
using yummyApp.Persistance.Services.Jwt;
using yummyApp.Application.Dtos.Users;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Tests.Controllers
{
    public class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<JwtAccountService> _accountServiceMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _configurationMock = new Mock<IConfiguration>();
            // Eğer JwtAccountService'in kurucu parametreleri varsa, bunları karşılayacak şekilde setup yapmanız gerekebilir.
            _accountServiceMock = new Mock<JwtAccountService>();
            _controller = new AuthController(_accountServiceMock.Object, _configurationMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task VerifyEmail_ReturnsOkResult_WithExpectedResponse()
        {
            // Arrange
            var request = new VerifyEmailCommandRequest
            {
                Email = "i.ako@example.com",
                ActivationCode = "r8Cyh3vcl+MV/Ty8NvQJwgPhr8kKJAaTjccthUUbQ/c="
            };

            var expectedResponse = new VerifyEmailCommandResponse
            {
                Succeeded = true,
                Message = "Email doğrulaması başarılı."
            };

            _mediatorMock
                .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.VerifyEmail(request) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task Login_ReturnsOkResult_WithExpectedResponse()
        {
            // Arrange
            var request = new UserLoginCommandRequest
            {
                UsernameOrEmail = "user@example.com",
                Password = "password123"
            };

            var token = new Token
            {
                AccessToken = "access-token",
                Expiration = DateTime.UtcNow.AddHours(1),
                RefreshToken = "refresh-token"
            };

            var expectedResponse = new UserLoginCommandResponse
            {
                AccessToken = token,
                Message = "Giriş başarılı.",
                UserName = "user"
            };

            _mediatorMock
                .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Login(request) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task Register_ReturnsOkResult_WithExpectedResponse()
        {
            // Arrange
            var request = new RegisterCommandRequest
            {
                Name = "John",
                Surname = "Doe",
                Email = "john.doe@example.com",
                Password = "Password123!",
                PasswordConfirm = "Password123!"
            };

            var expectedResponse = new RegisterCommandResponse
            {
                Message = "Kayıt başarılı.",
                Success = true
            };

            _mediatorMock
                .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Register(request) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task ResetPassword_ReturnsOkResult_WithExpectedResponse()
        {
            // Arrange
            // PasswordResetCommandRequest modelinizin gerekli property'lerini doldurun.
            var request = new PasswordResetCommandRequest
            {
                // Örneğin: Email = "user@example.com"
            };

            var expectedResponse = new PasswordResetCommandResponse
            {
                Message = "Şifre sıfırlama bağlantısı gönderildi.",
                Success = true
            };

            _mediatorMock
                .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.ResetPassword(request) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task UpdatePassword_ReturnsOkResult_WithExpectedResponse()
        {
            // Arrange
            var request = new NewPasswordCommandRequest
            {
                UserId = "user-id",
                NewPassword = "NewPassword123!",
                Token = "reset-token"
            };

            var expectedResponse = new NewPasswordCommandResponse
            {
                Message = "Şifre başarıyla güncellendi.",
                Success = true
            };

            _mediatorMock
                .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.UpdatePassword(request) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }
    }
}
