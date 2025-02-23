using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.UserFeedBacks.Queries.GetById
{
    public class GetByIdUserFeedBackQueryHandler : IRequestHandler<GetByIdUserFeedBackQueryRequest, GetByIdUserFeedBackQueryResponse>
    {
        readonly IMapper _mapper;
        readonly IUserFeedBackRepository _repository;

        public GetByIdUserFeedBackQueryHandler(IMapper mapper, IUserFeedBackRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GetByIdUserFeedBackQueryResponse> Handle(GetByIdUserFeedBackQueryRequest request, CancellationToken cancellationToken)
        {
            UserFeedback? data = await _repository.GetAsync(x => x.Id == request.Id,
    include: q => q.Include(c => c.User).Include(c => c.User));
            var mappedData = _mapper.Map< GetByIdUserFeedBackQueryResponse>(data);
            if (mappedData != null) return mappedData; else return new();
        }
    }
}
