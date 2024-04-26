using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;
using yummyApp.Domain.Events;

namespace yummyApp.Application.Features.Likes.Commands.Create
{
    public class CreateLikecommandHandler : IRequestHandler<CreateLikecommandRequest, CreateLikecommandResponse>
    {
        readonly IMapper _mapper;
        readonly ILikeRepository _likeRepository;

        public CreateLikecommandHandler(IMapper mapper, ILikeRepository likeRepository
            )
        {
            _mapper = mapper;
            _likeRepository = likeRepository;
        }

        public async Task<CreateLikecommandResponse> Handle(CreateLikecommandRequest request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Like>(request);
            await _likeRepository.AddAsync(data);
            data.AddDomainEvent(new LikeCreatedEvent(data));
            return new();
        }
    }
}
