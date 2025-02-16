using MediatR;
using yummyApp.Application.Dtos.GoogleApis;
using yummyApp.Application.Services.GoogleApi;

namespace yummyApp.Application.Features.Apis.Queries.GetNearByPlaces
{
    public class GetNearbyPlacesQueryHandler : IRequestHandler<GetNearbyPlacesQueryRequest, PlaceSearchResult>
    {
        readonly IGooglePlacesService _googlePlacesService;

        public GetNearbyPlacesQueryHandler(IGooglePlacesService googlePlacesService)
        {
            _googlePlacesService = googlePlacesService;
        }

        public async Task<PlaceSearchResult> Handle(GetNearbyPlacesQueryRequest request, CancellationToken cancellationToken)
        {
            var datas = await _googlePlacesService.GetNearbyPlacesAsync(request.Category, request.Latitude, request.Longitude, request.Radius);
            if (datas != null) return new PlaceSearchResult() { Results = datas.Results };
            else return new();
        }
    }
}
