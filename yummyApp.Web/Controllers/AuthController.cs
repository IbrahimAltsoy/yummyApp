using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using yummyApp.Application.Features.Users.Commands.NewPassword;
using yummyApp.Application.Features.Users.Commands.PasswordReset;
using yummyApp.Application.Features.Users.Commands.VerifyEmail;

namespace yummyApp.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;

        public AuthController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
        // mobil projeyi değiştir, Google ve Apple  ile giriş işlemi kaldır, Create Acoount ve ForgotPasssword ü düzelt akabinde UpdatePassword tarafını düzelt 
        public async Task<IActionResult> VerifyEmail(string email, string activationCode)
        {
            var request = new VerifyEmailCommandRequest
            {
                Email = email,
                ActivationCode = activationCode
            };
            var requestUri = _configuration["ApplicationSettings:ApiApplication"]!+ $"/api/Auth/verify-email?Email={email}&activationCode={activationCode}";
            var response = await _httpClient.GetAsync(requestUri);
            
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<VerifyEmailCommandResponse>(responseData);
                
                ViewBag.Message = result!.Message; 
                return View();
            }
            else
            {
                ViewBag.Error = "E-posta doğrulama işlemi başarısız oldu.";
                return View();
            }
        }

        [HttpGet("Auth/updatepassword/{userId}/{token}")]
        public IActionResult UpdatePassword(string userId, string token)
        {
           
            // userId ve token'i kullanarak model oluştur
            var model = new NewPasswordCommandRequest
            {
                UserId = userId,
                Token = token
            };

            // View'i göster
            return View(model);
        }

        // Yeni şifreyi işleyen ve API'ye gönderen Action
        [HttpPost("Auth/updatepassword/{userId}/{token}")]
        public async Task<IActionResult> UpdatePassword(string userId, string token, NewPasswordCommandRequest request)
        {
            if (!ModelState.IsValid)
            {
                // Model doğrulama hatası varsa formu tekrar göster
                return View(request);
            }

            // E-posta linkinden gelen userId ve token'i request'e ekle
            request.UserId = userId;
            request.Token = token;

            // API'ye istek gönder
            var response = await _httpClient.PostAsJsonAsync(_configuration["ApplicationSettings:ApiApplication"]!+ "/api/Auth/update-password", request);

            if (response.IsSuccessStatusCode)
            {
                // Başarılı durumda kullanıcıyı bilgilendir
                ViewBag.Message = "Şifreniz başarıyla güncellendi.";
                return View();
            }
            else
            {
                // Hata durumunda kullanıcıyı bilgilendir
                ViewBag.Error = "Şifre güncelleme işlemi başarısız oldu.";
                return View(request);
            }
        }
    }
}
