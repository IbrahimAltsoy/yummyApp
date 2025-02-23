using MediatR;

namespace yummyApp.Application.Features.UserFeedBacks.Commands.Update
{
    public class UpdateUserFeedBackCommandRequest:IRequest<UpdateUserFeedBackCommandResponse>
    {
        public Guid Id { get; set; }
        public bool IsAddressed { get; set; }
    }
}
