using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using yummyApp.Domain.Identity;

namespace yummyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
         readonly RoleManager<UserRole> _roleManager;

        public TestController(RoleManager<UserRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [AllowAnonymous]
        [HttpPost("addRoles")]
        public async Task<IActionResult> AddRoles()
        {
            var sonuc = 5;
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
