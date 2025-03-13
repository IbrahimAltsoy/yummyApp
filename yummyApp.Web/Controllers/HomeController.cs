using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;
using yummyApp.Application.Features.Users.Commands.VerifyEmail;
using yummyApp.Application.Features.Users.Queries.GetAll;
using yummyApp.Application.Responses;
using yummyApp.Web.Models;

namespace yummyApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;


        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            var apiUrl = _configuration["ApplicationSettings:ApiApplication"] + $"/user/";

            // API'den verileri çek
            var response = await _httpClient.GetAsync(apiUrl);

            // Baþarýlý bir yanýt alýndýysa
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetAllUserQueryResponse>(responseData);

                // Kullanýcýlarý ViewBag'e atayýn

                ViewBag.Users = result.TotalUserCount; // result null deðilse Users'ý ata
            }
            else
            {
                // Hata durumunda loglama veya hata mesajý gösterme iþlemleri yapýlabilir
                ViewBag.Users = 15; // Boþ bir liste atayýn veya hata mesajý gösterin
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
