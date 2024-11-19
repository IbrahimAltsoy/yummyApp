using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yummyApp.Application.Features.Comments.Queries.GetAll;
using yummyApp.Application.Repositories.Repository;

namespace yummyApp.Application.Features.Comments.Queries.GetById
{
    public class GetByIdCommentQueryHandler : IRequestHandler<GetByIdCommentQueryRequest, GetByIdCommentQueryResponse>
    {
        readonly IMapper _mapper;
        readonly ICommentRepository _commentRepository;

        public GetByIdCommentQueryHandler(IMapper mapper, ICommentRepository commentRepository)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        public async Task<GetByIdCommentQueryResponse> Handle(GetByIdCommentQueryRequest request, CancellationToken cancellationToken)
        {
            //var data = await _commentRepository.GetAsync(x=>x.Id==request.Id);
            var data = await _commentRepository.GetAsync(x => x.Id == request.Id,
    include: q => q.Include(c => c.User).Include(c => c.Post));
            var mappedData = _mapper.Map<GetByIdCommentQueryResponse>(data);
            return mappedData;
           
        }
    }
}
