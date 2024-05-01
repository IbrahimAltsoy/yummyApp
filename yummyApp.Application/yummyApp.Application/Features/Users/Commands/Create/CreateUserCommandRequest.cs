using MediatR;
using yummyApp.Domain.Entities;
using yummyApp.Domain.Enums;

namespace yummyApp.Application.Features.Users.Commands.Create
{
    public class CreateUserCommandRequest:IRequest<CreateUserCommandResponse>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
        public string ConfirmPhone { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsActive { get; set; }
        public Gender Gender { get; set; }
    }
}
