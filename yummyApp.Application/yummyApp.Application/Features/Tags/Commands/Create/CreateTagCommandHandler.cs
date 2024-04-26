using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;
using yummyApp.Domain.Events;

namespace yummyApp.Application.Features.Tags.Commands.Create
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommandRequest, CreateTagCommandResponse>
    {
        readonly IMapper _mapper;
        readonly ITagRepository _tagRepository;

        public CreateTagCommandHandler(IMapper mapper, ITagRepository tagRepository)
        {
            _mapper = mapper;
            _tagRepository = tagRepository;
        }

        public async Task<CreateTagCommandResponse> Handle(CreateTagCommandRequest request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Tag>(request);
            await _tagRepository.AddAsync(data);
            data.AddDomainEvent(new TagCreatedEvent(data));
            return new();
        }
    }
}
