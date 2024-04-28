using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Posts.Commands.Update
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommandRequest, UpdatePostCommandResponse>
    {
        readonly IMapper _mapper;
        readonly IPostRepository _repository;

        public UpdatePostCommandHandler(IMapper mapper, IPostRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<UpdatePostCommandResponse> Handle(UpdatePostCommandRequest request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Post>(request);
            await _repository.UpdateAsync(data);
            return new();
        }
    }
}
