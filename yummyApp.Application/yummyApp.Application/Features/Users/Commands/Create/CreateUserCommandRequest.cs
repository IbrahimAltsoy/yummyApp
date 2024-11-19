using MediatR;
using yummyApp.Domain.Entities;
using yummyApp.Domain.Enums;

namespace yummyApp.Application.Features.Users.Commands.Create
{
    public class CreateUserCommandRequest:IRequest<CreateUserCommandResponse>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public Gender Gender { get; set; }
        //public bool? IsActive { get; set; }
        //public string ActivationCode { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
