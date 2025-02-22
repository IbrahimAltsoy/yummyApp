using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;

namespace yummyApp.Application.Features.UserFeedBacks.Commands.Create
{
    public class CreateUserFeedBackCommandHandler : IRequestHandler<CreateUserFeedBackCommandRequest, CreateUserFeedBackCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IUserFeedBackRepository _repository;

        public CreateUserFeedBackCommandHandler(IMapper mapper, IUserFeedBackRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public Task<CreateUserFeedBackCommandResponse> Handle(CreateUserFeedBackCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
