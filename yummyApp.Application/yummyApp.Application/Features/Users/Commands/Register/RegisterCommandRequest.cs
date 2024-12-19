using MediatR;
using yummyApp.Domain.Enums;
using yummyApp.Domain.Identity;

namespace yummyApp.Application.Features.Users.Commands.Register
{
    public class RegisterCommandRequest:IRequest<RegisterCommandResponse>
    {

        public string Name { get; set; }        
        public string Surname { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

    }
}
