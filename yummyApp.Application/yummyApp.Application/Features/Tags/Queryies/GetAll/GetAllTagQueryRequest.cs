using MediatR;

namespace yummyApp.Application.Features.Tags.Queryies.GetAll
{
    public class GetAllTagQueryRequest:IRequest<IList<GetAllTagQueryResponse>>
    {
        string PostTitle { get; set; }
        string BusinessName { get; set; }
    }
}