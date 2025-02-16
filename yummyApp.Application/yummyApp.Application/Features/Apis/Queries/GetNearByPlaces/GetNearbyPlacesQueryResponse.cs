using yummyApp.Application.Dtos.GoogleApis;

namespace yummyApp.Application.Features.Apis.Queries.GetNearByPlaces
{
    public class GetNearbyPlacesQueryResponse
    {
        public PlaceSearchResult GoogleNearPlaces { get; set; }
    }
}
