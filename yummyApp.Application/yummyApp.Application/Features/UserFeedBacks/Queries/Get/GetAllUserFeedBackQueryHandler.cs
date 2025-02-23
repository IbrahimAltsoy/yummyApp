using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.UserFeedBacks.Queries.Get
{
    public class GetAllUserFeedBackQueryHandler : IRequestHandler<GetAllUserFeedBackQueryRequest, GetAllUserFeedBackQueryResult>
    {
        readonly IMapper _mapper;
        readonly IUserFeedBackRepository _repository;

        public GetAllUserFeedBackQueryHandler(IMapper mapper, IUserFeedBackRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GetAllUserFeedBackQueryResult> Handle(GetAllUserFeedBackQueryRequest request, CancellationToken cancellationToken)
        {
            var datas = await _repository.GetPaginateListAsync(x=>x.DeletedAt==null,include: x=>x.Include(x=>x.User), orderBy: c => c.OrderBy(c => c.CreatedAt),
                size: request.Size,
                index: request.Page,
                cancellationToken: cancellationToken);
            var feedbacks = _mapper.Map<IList< GetAllUserFeedBackQueryResponse>>(datas.Items);
            return new GetAllUserFeedBackQueryResult { TotalUserFeedbackCount = feedbacks.Count, UserFeedbacks = feedbacks.ToList()};
        }
    }
}
