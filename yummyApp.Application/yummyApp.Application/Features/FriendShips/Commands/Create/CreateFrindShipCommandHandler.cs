using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;
using yummyApp.Domain.Events;

namespace yummyApp.Application.Features.FriendShips.Commands.Create
{
    public class CreateFrindShipCommandHandler : IRequestHandler<CreateFrindShipCommandRequest, CreateFrindShipCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IFriendShipRepository _friendShipRepository;

        public CreateFrindShipCommandHandler(IMapper mapper, IFriendShipRepository friendShipRepository)
        {
            _mapper = mapper;
            _friendShipRepository = friendShipRepository;
        }

        public async Task<CreateFrindShipCommandResponse> Handle(CreateFrindShipCommandRequest request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Friendship>(request);
            await _friendShipRepository.AddAsync(data);
            data.AddDomainEvent(new FriendshipCreatedEvent(data));
            return new();
        }
    }
}
