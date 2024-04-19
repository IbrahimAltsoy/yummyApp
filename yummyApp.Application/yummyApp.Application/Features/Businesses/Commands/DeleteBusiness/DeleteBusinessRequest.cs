using MediatR;

namespace yummyApp.Application.Features.Businesses.Commands.DeleteBusiness
{
    public class DeleteBusinessRequest:IRequest<DeleteBusinessResponse>
    {
        public Guid Id { get; set; }
    }
}
