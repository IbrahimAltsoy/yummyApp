namespace yummyApp.Application.Features.Comments.Queries.GetById
{
    public class GetByIdCommentQueryResponse
    {
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string PostContent { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime Timestamp { get; set; }
    }
}