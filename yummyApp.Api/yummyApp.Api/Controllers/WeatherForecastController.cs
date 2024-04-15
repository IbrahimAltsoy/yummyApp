using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Text.Json;
using System.Threading;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Application.Paging;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;
using yummyApp.Persistance.Context;

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
        readonly IBusinessRepository _repository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IYummyAppDbContext appDbContext, IBusinessRepository repository)
        {
            _logger = logger;
            _appDbContext = appDbContext;
            _repository = repository;
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
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{


        //    //var entity = await _appDbContext.Businesses.Select(u => new
        //    //{
        //    //    u.Name,
        //    //    u.City,
        //    //    u.Description,
        //    //    u.Phone,
        //    //    u.CreatedAt,
        //    //    u.Address

        //    //}).ToListAsync();

        //    // Business? entities = await _repository.GetAsync(c => c.Id == id);
        //    //if (entity == null)
        //    //{
        //    //    return Ok("veritabaný boþ");
        //    //}
        //    //var entities = await _repository.GetListAsync();
        //    //if (entities == null || !entities.Any())
        //    //{
        //    //    return NotFound("Veritabaný boþ veya belirtilen sayfa bulunamadý.");
        //    //}
        //    //var simplifiedList = entities.Select(entity => new { Name = entity.Name, City = entity.City }).ToList();

        //    //var x = 5;

        //    //return Ok(simplifiedList);
        //    //IPaginate<Business> list =await _repository.GetPaginateListAsync();
        //    //var simple = 5;
        //    //return Ok(list);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Post()
        //{
        //    Business business = await _repository.AddAsync(new()
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Kodunuzu Basitleþtirin: Sorunun kaynaðýný belirlemek için kodunuzu basitleþtirin. Örneðin, include kýsmýný kaldýrarak sadece GetList metodunu çaðýrýn ve sonucu kontrol edin. Eðer sorunun kaynaðýný bulursanýz, koda geri dönerek sorunu giderin.",
        //        Address = "Van",
        //        Phone = "5345455555",
        //        City = "Van/Ýpekyolu",
        //        Description = "Harika",
        //        CreatedAt = DateTime.Now,
        //        Menu = ["Döner"],
        //        Quality = Domain.Enums.BusinessQuality.Good,
        //        CreatedBy = "Ibrahim"
        //    });

        //    return Ok(business);
        //}
        //[HttpPut]
        //public async Task<IActionResult> Put(Guid id)
        //{
        //    var entity = await _repository.GetAsync(x=>x.Id == id);
        //    entity.Name = "Faruk";
        //    entity.City = "Diyarbakýr";
        //    Business business = await _repository.UpdateAsync(entity);
        //    return Ok(business);
        //}
        //[HttpDelete]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var entity = await _repository.GetAsync(x=> x.Id == id);
        //    await _repository.DeleteAsync(entity);

        //    return Ok();

        //}
    }
}
// Not1: GetList Çalýþtýrýalamadý 
// Not2: GetPaginateList Çalýþmýyor:) 
