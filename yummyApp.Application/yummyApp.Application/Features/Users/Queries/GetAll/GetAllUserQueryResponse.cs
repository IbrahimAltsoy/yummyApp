namespace yummyApp.Application.Features.Users.Queries.GetAll
{
    public class GetAllUserQueryResponse
    {
        public int TotalUserCount { get; set; }
        public object Users { get; set; }
    }
}