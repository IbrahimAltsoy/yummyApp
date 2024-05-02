using AutoMapper;
using MediatR;
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
            var comment = await _commentRepository.GetAsync(x => x.Id == request.Id);
            comment.Content = request.Content;
            comment.Title = request.Title;
            comment.LikeCount = request.LikeCount;
            await _commentRepository.UpdateAsync(comment);
            return new();
        }
    }
}
