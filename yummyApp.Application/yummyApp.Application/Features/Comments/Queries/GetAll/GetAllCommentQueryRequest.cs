using MediatR;

namespace yummyApp.Application.Features.Comments.Queries.GetAll
{
    public class GetAllCommentQueryRequest:IRequest<IList<GetAllCommentQueryResponse>>
    {
    }
}