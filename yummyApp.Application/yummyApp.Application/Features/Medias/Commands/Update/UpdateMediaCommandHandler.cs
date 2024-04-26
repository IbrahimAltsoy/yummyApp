using MediatR;
using yummyApp.Application.Repositories.Repository;

namespace yummyApp.Application.Features.Medias.Commands.Update
{
    public class UpdateMediaCommandHandler : IRequestHandler<UpdateMediaCommandRequest, UpdateMediaCommandResponse>
    {
        readonly IMediaRepository _mediaRepository;

        public UpdateMediaCommandHandler(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public async Task<UpdateMediaCommandResponse> Handle(UpdateMediaCommandRequest request, CancellationToken cancellationToken)
        {
            await _mediaRepository.AddPhotoToPostAsync(request.Id, request.NewUrl);
            return new();
        }
    }
}
