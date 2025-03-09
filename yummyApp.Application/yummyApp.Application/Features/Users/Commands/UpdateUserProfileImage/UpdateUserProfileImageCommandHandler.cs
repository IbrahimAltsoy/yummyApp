using MediatR;
using yummyApp.Application.Abstract.Common;
using yummyApp.Application.Services.Users;

namespace yummyApp.Application.Features.Users.Commands.UpdateUserProfileImage
{
    public class UpdateUserProfileImageCommandHandler : IRequestHandler<UpdateUserProfileImageCommandRequest, UpdateUserProfileImageCommandResponse>
    {
        readonly IUserService _userService;
        readonly IUser _currentUser;

        public UpdateUserProfileImageCommandHandler(IUserService userService, IUser currentUser)
        {
            _userService = userService;
            _currentUser = currentUser;
        }

        public async Task<UpdateUserProfileImageCommandResponse> Handle(UpdateUserProfileImageCommandRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(_currentUser.Id, out Guid userId))
            {
                throw new ArgumentException("Current user ID is not a valid GUID.");
            }
           
            bool result = await _userService.UpdateUserProfileImageAsync(userId, request.Image);
            if (!result){return new UpdateUserProfileImageCommandResponse { Message = "Fotoğraf güncellenemedi", Success = false };}
            return new UpdateUserProfileImageCommandResponse { Message = "Fotoğrafınız güncellendi", Success = true };
        }
    }
}
