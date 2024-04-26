using MediatR;
using yummyApp.Application.Repositories.Repository;

namespace yummyApp.Application.Features.Medias.Queries.GetAll
{
    public class GetAllMediaQueryHandler : IRequestHandler<GetAllMediaQueryRequest, GetAllMediaQueryResponse>
    {
        readonly IMediaRepository _mediaRepository;

        public GetAllMediaQueryHandler(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public async Task<GetAllMediaQueryResponse> Handle(GetAllMediaQueryRequest request, CancellationToken cancellationToken)
        {
           var data = await _mediaRepository.GetAsync(x => x.Id == request.Id);
            return new GetAllMediaQueryResponse
            {
                Urls = data.Urls
            };

        }
    }
}
