using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yummyApp.Application.Features.UserFeedBacks.Queries.Get;
using yummyApp.Application.Repositories;
using yummyApp.Application.Repositories.Repository;

namespace yummyApp.Application.Features.UserFeedBacks.Queries.GetIsAddressed
{
    public class GetIsAddressedUserFeedbackQueryHandler : IRequestHandler<GetIsAddressedUserFeedbackQueryRequest, GetAllUserFeedBackQueryResult>
    {
        readonly IMapper _mapper;
        readonly IUserFeedBackRepository _repository;

        public GetIsAddressedUserFeedbackQueryHandler(IMapper mapper, IUserFeedBackRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GetAllUserFeedBackQueryResult> Handle(GetIsAddressedUserFeedbackQueryRequest request, CancellationToken cancellationToken)
        {
            var datas = await _repository.GetPaginateListAsync(x => x.DeletedAt == null && x.IsAddressed==false, include: x => x.Include(x => x.User), orderBy: c => c.OrderBy(c => c.CreatedAt),
                size: request.Size,
                index: request.Page,
                cancellationToken: cancellationToken);
            var feedbacks = _mapper.Map<IList<GetAllUserFeedBackQueryResponse>>(datas.Items);
            return new GetAllUserFeedBackQueryResult { TotalUserFeedbackCount = feedbacks.Count, UserFeedbacks = feedbacks.ToList() };
        }
    }
}
