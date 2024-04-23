using MediatR;

namespace yummyApp.Application.Features.Comments.Queries.GetById
{
    public class GetByIdCommentQueryRequest:IRequest<GetByIdCommentQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
