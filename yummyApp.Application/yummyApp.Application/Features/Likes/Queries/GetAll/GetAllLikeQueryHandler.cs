using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Application.Responses;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Likes.Queries.GetAll
{
    public class GetAllLikeQueryHandler : IRequestHandler<GetAllLikeQueryRequest, List<GetAllLikeQueryResponse>>
    {
        readonly IMapper _mapper;
        readonly ILikeRepository _likeRepository;
        readonly IYummyAppDbContext _dbContext;

        public GetAllLikeQueryHandler(IMapper mapper, ILikeRepository likeRepository, IYummyAppDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _likeRepository = likeRepository;
        }

        public async Task<List<GetAllLikeQueryResponse>> Handle(GetAllLikeQueryRequest request, CancellationToken cancellationToken)
        {
            var usersWhoLiked = await _likeRepository.UsersWhoLikedAsync(request.PostId);

            var responseList = usersWhoLiked.Select(user => new GetAllLikeQueryResponse
            {
                Name = user.Name,
                Surname = user.Surname,
                //LikeCount = usersWhoLiked.Count // beğeni sayısı için ayrı bir handle sınıfı oluşturabilirsin
            }).ToList();
            return responseList;
        }
    }
}
//var data = _mapper.Map<Comment>(request);

//var likes = await _dbContext.Likes
//.Where(l => l.PostID == request.PostId)
//.Select(l => l.UserID)
//.ToListAsync();

//var usersWhoLiked = await _dbContext.Users
//    .Where(u => likes.Contains(u.Id))
//    .Select(u => new GetAllLikeQueryResponse
//    {
//        Name = u.Name,
//        Surname = u.Surname,
//        LikeCount = likes.Count() // başka yerden alabilirsin bunları o şekilde basabilirsin
//    })
//    .ToListAsync();