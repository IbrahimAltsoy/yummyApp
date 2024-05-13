using MediatR;

namespace yummyApp.Application.Features.Users.Queries.GetAll
{
    public class GetAllUserQueryRequest:IRequest<GetAllUserQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}