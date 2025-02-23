using yummyApp.Domain.Enums;

namespace yummyApp.Application.Features.UserFeedBacks.Queries.Get
{
    public class GetAllUserFeedBackQueryResult
    {
        public int TotalUserFeedbackCount { get; set; }
        public IList<GetAllUserFeedBackQueryResponse> UserFeedbacks { get; set; }
    }
    public class GetAllUserFeedBackQueryResponse
    {        
        //public Guid? UserID { get; set; }
        public string? UserName { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? Message { get; set; }
        public UserFeedbackEnum UserFeedbackEnum { get; set; }
        public string? Email { get; set; }
        public bool? IsAddressed { get; set; }

    }
}
