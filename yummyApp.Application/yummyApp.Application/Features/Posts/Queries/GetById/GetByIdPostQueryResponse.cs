using yummyApp.Domain.Enums;

namespace yummyApp.Application.Features.Posts.Queries.GetById
{
    public class GetByIdPostQueryResponse
    {
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string? Title { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public PostQuality Quality { get; set; }
        public int Rating { get; set; }
    }
}