using MediatR;
using Microsoft.AspNetCore.Http;

namespace yummyApp.Application.Features.Users.Commands.UpdateUserProfileImage
{
    public class UpdateUserProfileImageCommandRequest:IRequest<UpdateUserProfileImageCommandResponse>
    {        
        public IFormFile Image { get; set; }       
    }
}
