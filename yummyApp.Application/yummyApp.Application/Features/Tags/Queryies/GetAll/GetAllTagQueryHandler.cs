using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;

namespace yummyApp.Application.Features.Tags.Queryies.GetAll
{
    public class GetAllTagQueryHandler : IRequestHandler<GetAllTagQueryRequest, IList<GetAllTagQueryResponse>>
    {
        readonly IMapper _mapper;
        readonly ITagRepository _tagRepository;

        public GetAllTagQueryHandler(IMapper mapper, ITagRepository tagRepository)
        {
            _mapper = mapper;
            _tagRepository = tagRepository;
        }

        public Task<IList<GetAllTagQueryResponse>> Handle(GetAllTagQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
