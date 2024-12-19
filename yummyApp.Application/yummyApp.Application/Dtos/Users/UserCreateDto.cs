using yummyApp.Domain.Enums;
using yummyApp.Domain.Identity;

namespace yummyApp.Application.Dtos.Users
{
    public class UserCreateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public Gender? Gender { get; set; }
        //public string ActivationCode { get; set; }
        //public bool? IsActive { get; set; }
        public string? Password { get; set; }
        public string? PasswordConfirm { get; set; }


        public Guid? RoleId { get; set; }
        public List<UserRole> Roles { get; set; }
    }
}
