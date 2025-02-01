using yummyApp.Application.Dtos.GoogleApis;
using yummyApp.Application.Dtos.GoogleApis.PlaceDetail;

namespace yummyApp.Application.Services.GoogleApi
{
    public interface IGooglePlacesService
    {
        Task<PlaceSearchResult> GetNearbyPlacesAsync(double latitude, double longitude, int radius = 15000);
        Task<PlaceDetailResult> GetPlaceReviews(string placeId, double? latitude, double? longitude);
    }
}
