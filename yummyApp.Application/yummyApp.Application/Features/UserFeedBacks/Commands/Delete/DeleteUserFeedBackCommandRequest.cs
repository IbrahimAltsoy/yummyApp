using MediatR;

namespace yummyApp.Application.Features.UserFeedBacks.Commands.Delete
{
    public class DeleteUserFeedBackCommandRequest:IRequest<DeleteUserFeedBackCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
