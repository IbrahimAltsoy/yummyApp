using Microsoft.AspNetCore.Identity;

namespace yummyApp.Infrastructure.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
