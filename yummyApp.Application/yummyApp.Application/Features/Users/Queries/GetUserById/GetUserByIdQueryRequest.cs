using MediatR;

namespace yummyApp.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryRequest :IRequest<GetUserByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}