using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Posts.Queries.GetById
{
    public class GetByIdPostQueryHandler : IRequestHandler<GetByIdPostQueryRequest, GetByIdPostQueryResponse>
    {
        readonly IMapper _mapper;
        readonly IPostRepository _postRepository;

        public GetByIdPostQueryHandler(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<GetByIdPostQueryResponse> Handle(GetByIdPostQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _postRepository.GetAsync(x => x.Id == request.Id);
            var mappedData = _mapper.Map<GetByIdPostQueryResponse>(data);
            return mappedData;
        }
    }
}
