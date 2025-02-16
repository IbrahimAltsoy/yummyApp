using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using yummyApp.Application.Services.GoogleApi;
using yummyApp.Domain.Identity;

namespace yummyApp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
         readonly RoleManager<UserRole> _roleManager;
        readonly IGooglePlacesService _googlePlacesService;

        public TestController(RoleManager<UserRole> roleManager, IGooglePlacesService googlePlacesService)
        {
            _roleManager = roleManager;
            _googlePlacesService = googlePlacesService;
        }
        [AllowAnonymous]
        [HttpGet("get-nearby-places")]
        public async Task<IActionResult> GetNearbyPlaces(double latitude, double longitude, string category, int radius)
        {
            var places = await _googlePlacesService.GetNearbyPlacesAsync(category, latitude, longitude, radius);

            return Ok(places.Results);
        }
        [AllowAnonymous]
        [HttpGet("get-business-detail/{id}")]
        public async Task<IActionResult> GetReviews([FromRoute] string id, [FromQuery] double? latitude, [FromQuery] double? longitude)
        {
            var places = await _googlePlacesService.GetPlaceReviews(id,latitude!.Value, longitude!.Value);

            return Ok(places);
        }
        [AllowAnonymous]
        [HttpPost("addRoles")]
        public async Task<IActionResult> AddRoles()
        {            
            var roles = new List<string> { "User", "TemporaryUser", "Admin", "Manager" }; // Eklemek istediğin roller

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    var result = await _roleManager.CreateAsync(new UserRole(role));
                    if (!result.Succeeded)
                    {
                        return BadRequest($"Rol eklenirken hata oluştu: {role}");
                    }
                }
            }
            return Ok("Roller başarıyla eklendi.");
        }
    }
}
