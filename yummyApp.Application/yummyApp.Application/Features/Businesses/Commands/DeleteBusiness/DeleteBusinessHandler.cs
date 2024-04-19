using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Businesses.Commands.DeleteBusiness
{
    public class DeleteBusinessHandler : IRequestHandler<DeleteBusinessRequest, DeleteBusinessResponse>
    {
        readonly IMapper _mapper;
        readonly IBusinessRepository _businessRepository;

        public DeleteBusinessHandler(IMapper mapper, IBusinessRepository businessRepository)
        {
            _mapper = mapper;
            _businessRepository = businessRepository;
        }

        public async Task<DeleteBusinessResponse> Handle(DeleteBusinessRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Business>(request);
            await _businessRepository.DeleteAsync(entity);
            return new()
            {
                Message="silme işlemi başarılı"
            };
        }
    }
}
