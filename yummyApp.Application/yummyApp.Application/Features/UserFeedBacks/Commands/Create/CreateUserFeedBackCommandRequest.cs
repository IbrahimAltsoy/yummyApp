using MediatR;
using yummyApp.Domain.Enums;

namespace yummyApp.Application.Features.UserFeedBacks.Commands.Create
{
    public class CreateUserFeedBackCommandRequest:IRequest<CreateUserFeedBackCommandResponse>
    {
        public Guid? UserID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public UserFeedbackEnum? UserFeedbackEnum { get; set; }
        public string? Email { get; set; }
        public bool? IsAddressed { get; set; }
    }
}
