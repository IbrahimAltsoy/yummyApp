using AutoMapper;
using MediatR;
using yummyApp.Application.Abstract.DbContext;
using yummyApp.Application.Repositories;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Events;
using yummyApp.Domain.Identity;

namespace yummyApp.Application.Features.Users.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IYummyAppDbContext _yummyAppDbContext;
        

        public CreateUserCommandHandler(IMapper mapper, IYummyAppDbContext yummyAppDbContext)
        {
            _mapper = mapper;
            _yummyAppDbContext = yummyAppDbContext;
            
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<AppUser>(request);
            //await _appUserRepository.AddAsync(data);
            await _yummyAppDbContext.AppUsers.AddAsync(data);
            await _yummyAppDbContext.SaveChangesAsync();
            //data.AddDomainEvent(new UserCreatedEvent(data));
            return new();
        }
    }
}
