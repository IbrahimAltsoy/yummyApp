using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Posts.Queries.GetAll
{
    public class GetAllPostQueryHandler : IRequestHandler<GetAllPostQueryRequest, IList<GetAllPostQueryResponse>>
    {
        readonly IMapper _mapper;
        readonly IPostRepository _postRepository;

        public GetAllPostQueryHandler(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<IList<GetAllPostQueryResponse>> Handle(GetAllPostQueryRequest request, CancellationToken cancellationToken)
        {
            
           var data = await _postRepository.GetListAsync(x=>x.DeletedAt==null);
            var mappeddata = _mapper.Map<IList<GetAllPostQueryResponse>>(data);
            return mappeddata;

        }
    }
}
