using AutoMapper;
using MediatR;
using yummyApp.Application.Abstract.Common;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;
using yummyApp.Domain.Events;

namespace yummyApp.Application.Features.UserFeedBacks.Commands.Create
{
    public class CreateUserFeedBackCommandHandler : IRequestHandler<CreateUserFeedBackCommandRequest, CreateUserFeedBackCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IUserFeedBackRepository _repository;
        readonly IUser _user;

        public CreateUserFeedBackCommandHandler(IMapper mapper, IUserFeedBackRepository repository, IUser user)
        {
            _mapper = mapper;
            _repository = repository;
            _user = user;
        }

        public async Task<CreateUserFeedBackCommandResponse> Handle(CreateUserFeedBackCommandRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(_user.Id, out Guid userId))
            {               
                throw new ArgumentException("Current user ID is not a valid GUID.");
            }
            
            request.UserID = userId;
            request.Email = _user.Email;
            var data = _mapper.Map<UserFeedback>(request);
            await _repository.AddAsync(data);
            data.AddDomainEvent(new UserFeedBackCreatedEvent(data));
            return new CreateUserFeedBackCommandResponse()
            {
                Message = "Feedback başarıyla dolduruldu.",
                Success = true
               
            };

        }
    }
}
