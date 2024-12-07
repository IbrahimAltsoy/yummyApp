using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using yummyApp.Application.Abstract.Common;
using yummyApp.Domain.Identity;
using yummyApp.Persistance.Services.Jwt;

namespace yummyApp.Persistance.Authentication
{
    public class CurrentUser : IUser
    {
         readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            
        }
        public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        
    }
}
