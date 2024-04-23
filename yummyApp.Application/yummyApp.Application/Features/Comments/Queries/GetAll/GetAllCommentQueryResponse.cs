namespace yummyApp.Application.Features.Comments.Queries.GetAll
{
    public class GetAllCommentQueryResponse
    {
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string PostContent { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}