using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;
using Google.Apis.Auth;
using yummyApp.Application.Helpers;
using yummyApp.Application.Abstract.Common;
using yummyApp.Application.Dtos.Users;
using yummyApp.Application.Exceptions;
using yummyApp.Application.Services.Authencation;
using yummyApp.Application.Services.Email;
using yummyApp.Application.Services.Users;
using yummyApp.Application.Tokens;
using yummyApp.Domain.Identity;
using yummyApp.Application.Exceptions.AuthExceptions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using yummyApp.Application.Features.Users.Commands.Register;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Application.Features.Users.Rules;
using Azure.Core;
using System.Net;
using yummyApp.Persistance.Services.Email;
using Microsoft.AspNetCore.WebUtilities;




namespace yummyApp.Persistance.Services.Authencation
{
    public class AuthService:IAuthService
    {
        readonly HttpClient _httpClient;
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly IConfiguration _configuration;
        readonly SignInManager<AppUser> _signInManager;
        readonly IUserService _userService;
        readonly IEmailService _emailService;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IYummyAppDbContext _yummyAppDbContext;
        readonly AuthBusinessRules _authBusinessRules;


        public AuthService(HttpClient httpClient, UserManager<AppUser> userManager, ITokenHandler tokenHandler, IConfiguration configuration, SignInManager<AppUser> signInManager, IUserService userService, IEmailService emailService, IHttpContextAccessor httpContextAccessor, IYummyAppDbContext yummyAppDbContext, AuthBusinessRules authBusinessRules)
        {
            _httpClient = httpClient;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _configuration = configuration;
            _signInManager = signInManager;
            _userService = userService;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _yummyAppDbContext = yummyAppDbContext;
            _authBusinessRules = authBusinessRules;
           
        }
        //public async Task<Token> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTime)
        //{
        //    AppUser? user = await _userManager.FindByNameAsync(userNameOrEmail);

        //    if (user == null)
        //        user = await _userManager.FindByEmailAsync(userNameOrEmail);
        //    if (user == null)
        //        throw new NotFoundUserExceptions();
        //    await _authBusinessRules.UserShouldBeExists(user);
        //    await _authBusinessRules.UserEmailVerifyCheck(user);
        //    SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        //    if (result.Succeeded)
        //    {

        //        //Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
        //        Token token =await  _tokenHandler.CreateAccessTokenAsync(accessTokenLifeTime, user);
        //        await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 5 * 60);

        //        return token;

        //    }
        //    throw new AuthenticationErrorExceptions("Email veya Şifre yanlış girdiniz.");
        //}
        public async Task<Token> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTime)
        {
            AppUser? user = await _userManager.FindByNameAsync(userNameOrEmail);

            if (user == null)
                user = await _userManager.FindByEmailAsync(userNameOrEmail);

            // Kullanıcı bulunamazsa veya şifre yanlışsa aynı hata mesajını döndür
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                throw new NotFoundUserExceptions("Email veya Şifre yanlış girdiniz.");
            }

            // Kullanıcı doğrulama işlemleri
            await _authBusinessRules.UserShouldBeExists(user);
            await _authBusinessRules.UserEmailVerifyCheck(user);

            // Token oluşturma ve refresh token güncelleme
            Token token = await _tokenHandler.CreateAccessTokenAsync(accessTokenLifeTime, user);
            await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 5 * 60);

            return token;
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                //Token token = _tokenHandler.CreateAccessToken(15 * 60, user);
                Token token =await _tokenHandler.CreateAccessTokenAsync(15 * 60, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 5 * 60);
                return token;
            }
            else
                throw new NotFoundUserExceptions();
        }

        public async Task ResetPasswordAsync(string email)
        {
            AppUser? user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                //resetToken = resetToken.UrlEncode();
                await _emailService.SendPasswordResetMailAsync(email, user.Id.ToString(), resetToken);

            }
            


        }

        public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {                
               //resetToken = resetToken.UrlDecode();
                return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
            }
            return false;
        }
        //public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
        //{
        //    var settings = new GoogleJsonWebSignature.ValidationSettings()
        //    {
        //        Audience = new List<string> { _configuration["Google:PROVIDER_ID"]! }
        //    };
        //    var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
        //    var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");


        //    AppUser? user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
        //    bool result = user != null;
        //    if (user == null)
        //    {
        //        user = await _userManager.FindByEmailAsync(payload.Email);
        //        if (user == null)
        //        {
        //            user = new AppUser()
        //            {                      
        //                Email = payload.Email,
        //                UserName = payload.Email,
        //                Surname = payload.Name,
        //                Name = payload.Name,
        //            };
        //            var identiyResult = await _userManager.CreateAsync(user);
        //            result = identiyResult.Succeeded;
        //        }
        //    }
        //    if (result)
        //    {
        //        await _userManager.AddLoginAsync(user, info);
        //        //Token token = _tokenHandler.CreateAccessToken(15 * 60, user);
        //        Token token =await _tokenHandler.CreateAccessTokenAsync(15 * 60, user);
        //        await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 5 * 60);
        //        return token;

        //    }
        //    else
        //    {
        //        throw new Exception("Invalid external authacation.");
        //    }



        //}
        public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
        {
            // 1️⃣ Google'ın public key'leriyle token doğrulama ayarlarını al
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["Authentication:Google:ClientId"]! } // Daha güvenli hale getirdik
            };

            try
            {
                // 2️⃣ Google Token'ı doğrula
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

                if (payload == null)
                    throw new Exception("Google kimlik doğrulaması başarısız.");

                var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

                // 3️⃣ Kullanıcı Google hesabıyla daha önce giriş yapmış mı kontrol et
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                if (user == null)
                {
                    // 4️⃣ Kullanıcı daha önce giriş yapmadıysa, e-posta ile kaydı var mı kontrol et
                    user = await _userManager.FindByEmailAsync(payload.Email);
                    if (user == null)
                    {
                        // Yeni bir kullanıcı oluştur
                        user = new AppUser()
                        {
                            Email = payload.Email,
                            UserName = payload.Email,
                            Surname = payload.FamilyName, // Soyadını FamilyName olarak güncelledik
                            Name = payload.GivenName      // Adını GivenName olarak güncelledik
                        };
                        var identityResult = await _userManager.CreateAsync(user);

                        if (!identityResult.Succeeded)
                            throw new Exception("Kullanıcı oluşturulurken hata oluştu.");
                    }

                    // Google hesabını kullanıcıya bağla
                    await _userManager.AddLoginAsync(user, info);
                }

                // 5️⃣ Kullanıcıya yeni bir JWT Token oluştur
                Token token = await _tokenHandler.CreateAccessTokenAsync(accessTokenLifeTime, user);

                // 6️⃣ Refresh token'ı güncelle
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 5 * 60);

                return token;
            }
            catch (Exception ex)
            {
                throw new Exception($"Google Login hatası: {ex.Message}");
            }
        }

        public async Task<RegisterCommandResponse> RegisterAsync(RegisterCommandRequest request)
        {
          
           
                string? activeCode = _tokenHandler.CreateRefreshToken();
                AppUser? user = new AppUser
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    UserName = request.Email,
                    IsActive = false,
                    ActivationCode = activeCode,
                    Email = request.Email
                };   
                
                IdentityResult result = await _userManager.CreateAsync(user, request.Password);

                RegisterCommandResponse response = new() { Success = result.Succeeded };
                if (response.Success)
                {
                    string encodedEmail = Uri.EscapeDataString(request.Email);
                    string encodedActivationCode = Uri.EscapeDataString(activeCode);
                string mobileBaseUrl = _configuration["ApplicationSettings:AdminApplication"]!;
                string activationLink = $"{mobileBaseUrl}/Auth/verifyemail?Email={encodedEmail}&ActivationCode={encodedActivationCode}";
                    await _emailService.SendMailAsync(
                        request.Email,
                        "Aktivasyon Kodu",
                        $"Kaydınızı doğrulamak için aşağıdaki bağlantıya tıklayınız: <a href='{activationLink}'>Aktivasyon Linki</a>"
                    );
                    await _userManager.AddToRoleAsync(user, "TemporaryUser");
                    response.Message = "Lütfen Email doğrulayınız!";
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        response.Message += $"{error.Description} - {error.Code}<br>";
                    }
                }
                return response;

        }

        public async Task<bool> ResetPasswordWithTokenAsync(string userId, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);
            if (!resetPasswordResult.Succeeded)
            {
                string errors = string.Join(", ", resetPasswordResult.Errors.Select(e => e.Description));
                return false;
            }
            return true;
        }
    }
}
