using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    }
}
