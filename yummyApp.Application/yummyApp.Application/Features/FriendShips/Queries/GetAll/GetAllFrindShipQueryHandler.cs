using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Application.Repositories.Repository;

namespace yummyApp.Application.Features.FriendShips.Queries.GetAll
{
    public class GetAllFrindShipQueryHandler : IRequestHandler<GetAllFrindShipQueryRequst, GetAllFrindShipQueryResponse>
    {
        readonly IMapper _mapper;
        readonly IFriendShipRepository _friendShipRepository;
        readonly IYummyAppDbContext _yummyAppDbContext;

        public GetAllFrindShipQueryHandler(IMapper mapper, IFriendShipRepository friendShipRepository, IYummyAppDbContext yummyAppDbContext)
        {
            _mapper = mapper;
            _friendShipRepository = friendShipRepository;
            _yummyAppDbContext = yummyAppDbContext;
        }

        public async Task<GetAllFrindShipQueryResponse> Handle(GetAllFrindShipQueryRequst request, CancellationToken cancellationToken)
        {
            var data = await _friendShipRepository.GetListAsync();
            //var mappedData = _mapper.Map<GetAllFrindShipQueryResponse>(data);
            var followee = await _yummyAppDbContext.Friendships.CountAsync(f => f.FolloweeID == request.Id);
            var follower = await _yummyAppDbContext.Friendships.CountAsync(f => f.FollowerID == request.Id);
            return new()
            {
                
                Followee = followee,
                Follower = follower

            };


        }
    }
}
