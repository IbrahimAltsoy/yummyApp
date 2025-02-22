using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;

namespace yummyApp.Application.Features.UserFeedBacks.Commands.Update
{
    public class UpdateUserFeedBackCommandHandler : IRequestHandler<UpdateUserFeedBackCommandRequest, UpdateUserFeedBackCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IUserFeedBackRepository _repository;

        public UpdateUserFeedBackCommandHandler(IMapper mapper, IUserFeedBackRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public Task<UpdateUserFeedBackCommandResponse> Handle(UpdateUserFeedBackCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
