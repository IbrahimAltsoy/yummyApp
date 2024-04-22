using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Businesses.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQueryRequest, GetByIdQueryResponse>
    {
        readonly IMapper _mapper;
        readonly IBusinessRepository _businessRepository;

        public GetByIdQueryHandler(IMapper mapper, IBusinessRepository businessRepository)
        {
            _mapper = mapper;
            _businessRepository = businessRepository;
        }

        public async Task<GetByIdQueryResponse> Handle(GetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _businessRepository.GetAsync(x=>x.Id == request.Id);
            var mappedData = _mapper.Map<GetByIdQueryResponse>(data);
            return mappedData;
        }
    }
}
