using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;
using yummyApp.Domain.Events;

namespace yummyApp.Application.Features.Medias.Commands.Create
{
    public class CreateMediaCommandHandler : IRequestHandler<CreateMediaCommandRequest, CreateMediaCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IMediaRepository _mediaRepository;

        public CreateMediaCommandHandler(IMapper mapper, IMediaRepository mediaRepository)
        {
            _mapper = mapper;
            _mediaRepository = mediaRepository;
        }

        public async Task<CreateMediaCommandResponse> Handle(CreateMediaCommandRequest request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Media>(request);
            await _mediaRepository.AddAsync(data);
            data.AddDomainEvent(new MediaCreatedEvent(data));
            return new();
        }
    }
}
