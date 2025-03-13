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

            // API'den verileri �ek
            var response = await _httpClient.GetAsync(apiUrl);

            // Ba�ar�l� bir yan�t al�nd�ysa
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetAllUserQueryResponse>(responseData);

                // Kullan�c�lar� ViewBag'e atay�n

                ViewBag.Users = result.TotalUserCount; // result null de�ilse Users'� ata
            }
            else
            {
                // Hata durumunda loglama veya hata mesaj� g�sterme i�lemleri yap�labilir
                ViewBag.Users = 15; // Bo� bir liste atay�n veya hata mesaj� g�sterin
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
