using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Posts.Commands.Delete
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommandRequest, DeletePostCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IPostRepository _postRepository;

        public DeletePostCommandHandler(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<DeletePostCommandResponse> Handle(DeletePostCommandRequest request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Post>(request);
            await _postRepository.DeleteAsync(data);
            return new();
        }
    }
}
