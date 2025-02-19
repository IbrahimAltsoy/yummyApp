using AutoMapper;
using MediatR;
using yummyApp.Application.Exceptions;
using yummyApp.Application.Repositories.Repository;

namespace yummyApp.Application.Features.Businesses.Queries.GetAll
{
    public class GetAllBusinessQueryHandler : IRequestHandler<GetAllBusinessQueryRequest, IList<GetAllBusinessQueryResponse>>
    {
        readonly IMapper _mapper;
        readonly IBusinessRepository _businessRepository;

        public GetAllBusinessQueryHandler(IMapper mapper, IBusinessRepository businessRepository)
        {
            _mapper = mapper;
            _businessRepository = businessRepository;
        }

        public async Task<IList<GetAllBusinessQueryResponse>> Handle(GetAllBusinessQueryRequest request, CancellationToken cancellationToken)
        {            
            var list = await _businessRepository.GetListAsync();
            var mappedList = _mapper.Map<IList<GetAllBusinessQueryResponse>>(list);
            return mappedList;
        }
    }
}
