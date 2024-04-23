using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Application.Repositories.Repository;

namespace yummyApp.Application.Features.Comments.Queries.GetAll
{
    public class GetAllCommentQueryHandler : IRequestHandler<GetAllCommentQueryRequest, IList<GetAllCommentQueryResponse>>
    {
        readonly IMapper _mapper;
        readonly ICommentRepository _commentRepository;
        readonly IYummyAppDbContext _dbContext;

        public GetAllCommentQueryHandler(IMapper mapper, ICommentRepository commentRepository, IYummyAppDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        public async Task<IList<GetAllCommentQueryResponse>> Handle(GetAllCommentQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _commentRepository.GetListAsync(include: q => q.Include(c => c.User).Include(c => c.Post));
            var mappedData = _mapper.Map<IList<GetAllCommentQueryResponse>>(data);
            return mappedData;
        }
    }
}
