using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;
using yummyApp.Domain.Events;

namespace yummyApp.Application.Features.Posts.Commands.Create
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommandRequest, CreatePostCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IPostRepository _repository;

        public CreatePostCommandHandler(IMapper mapper, IPostRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CreatePostCommandResponse> Handle(CreatePostCommandRequest request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Post>(request);
            await _repository.AddAsync(data);
            data.AddDomainEvent(new PostCreatedEvent(data));
            return new();

        }
    }
}
