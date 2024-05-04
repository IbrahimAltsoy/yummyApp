using Microsoft.AspNetCore.Identity;

namespace yummyApp.Domain.Identity
{
    public class UserRole : IdentityRole<Guid>
    {
        public UserRole()
        {
        }

        public UserRole(string roleName) : base(roleName)
        {
        }

    }
}
