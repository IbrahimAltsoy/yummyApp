using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;

namespace yummyApp.Application.Features.UserFeedBacks.Commands.Delete
{
    public class DeleteUserFeedBackCommandHandler : IRequestHandler<DeleteUserFeedBackCommandRequest, DeleteUserFeedBackCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IUserFeedBackRepository _repository;

        public DeleteUserFeedBackCommandHandler(IMapper mapper, IUserFeedBackRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public Task<DeleteUserFeedBackCommandResponse> Handle(DeleteUserFeedBackCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
