using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yummyApp.Application.Abstract.DbContext;

namespace yummyApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        readonly IYummyAppDbContext _appDbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IYummyAppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
        [HttpGet]
        public async Task<IActionResult> Business()
        {
            var entity = await _appDbContext.Businesses.Select(u=> new
            {
                u.Name,
                u.City,
                u.Description,
                u.Phone,
                u.CreatedAt,
                u.Address
                
            }).ToListAsync();
            if (entity == null)
            {
                return Ok("veritabaný boþ");
            }

            return Ok(entity);
        }
    }
}
