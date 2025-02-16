using MediatR;
using yummyApp.Application.Dtos.GoogleApis.PlaceDetail;
using yummyApp.Application.Services.GoogleApi;

namespace yummyApp.Application.Features.Apis.Queries.GetNearByPlaceDetail
{
    public class GetNearByPlaceDetailQueryHandler : IRequestHandler<GetNearByPlaceDetailQueryRequest, PlaceDetailResult>
    {
        readonly IGooglePlacesService _googlePlacesService;

        public GetNearByPlaceDetailQueryHandler(IGooglePlacesService googlePlacesService)
        {
            _googlePlacesService = googlePlacesService;
        }

        public async Task<PlaceDetailResult> Handle(GetNearByPlaceDetailQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _googlePlacesService.GetPlaceDetailAndReviews(request.PlaceId!, request.Latitude, request.Longitude);
            if (data != null) return new PlaceDetailResult() { Result = data.Result };
            else return new();
        }
    }
}
