using yummyApp.Domain.Enums;

namespace yummyApp.Application.Features.UserFeedBacks.Queries.GetById
{
    public class GetByIdUserFeedBackQueryResponse
    {
        public string? UserName { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? Message { get; set; }
        public UserFeedbackEnum UserFeedbackEnum { get; set; }
        public string? Email { get; set; }
        public bool? IsAddressed { get; set; }
    }
}
