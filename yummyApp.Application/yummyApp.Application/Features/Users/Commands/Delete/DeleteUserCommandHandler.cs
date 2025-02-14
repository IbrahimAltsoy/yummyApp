using MediatR;
using Microsoft.AspNetCore.Identity;
using yummyApp.Application.Services.Users;

namespace yummyApp.Application.Features.Users.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, DeleteUserCommandResponse>
    {
        readonly IUserService _userService;

        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
        {
            bool state = await _userService.SoftDeleteUserAsync(request.Id);
            if (state) return new() { Message ="Kullanıcı başarıyla silindi.", Success = true };
            else return new() { Message= "Kullanıcı bulunamadı.",Success = false };
        }
    }
}
