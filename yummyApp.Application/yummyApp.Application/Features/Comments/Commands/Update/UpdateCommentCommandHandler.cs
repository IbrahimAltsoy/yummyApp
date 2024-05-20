using AutoMapper;
using MediatR;
using yummyApp.Application.Repositories;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Comments.Commands.Update
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommandRequest, UpdateCommentCommandResponse>
    {
        readonly IMapper _mapper;
        readonly ICommentRepository _commentRepository;

        public UpdateCommentCommandHandler(IMapper mapper, ICommentRepository commentRepository)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        public async Task<UpdateCommentCommandResponse> Handle(UpdateCommentCommandRequest request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Comment>(request);
            await _commentRepository.UpdateAsync(data);
            return new();
        }
    }
}
