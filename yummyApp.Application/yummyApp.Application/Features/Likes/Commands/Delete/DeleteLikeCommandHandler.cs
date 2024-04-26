using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Likes.Commands.Delete
{
    public class DeleteLikeCommandHandler : IRequestHandler<DeleteLikeCommandRequest, DeleteLikeCommandResponse>
    {
        readonly IMapper _mapper;
        readonly ILikeRepository _likeRepository;

        public DeleteLikeCommandHandler(IMapper mapper, ILikeRepository likeRepository)
        {
            _mapper = mapper;
            _likeRepository = likeRepository;
        }

        public async Task<DeleteLikeCommandResponse> Handle(DeleteLikeCommandRequest request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Like>(request);
            await _likeRepository.DeleteAsync(data);
            return new();
        }
    }
}
