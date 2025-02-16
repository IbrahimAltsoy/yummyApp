using yummyApp.Application.Dtos.GoogleApis;
using yummyApp.Application.Dtos.GoogleApis.PlaceDetail;

namespace yummyApp.Application.Services.GoogleApi
{
    public interface IGooglePlacesService
    {
        Task<PlaceSearchResult> GetNearbyPlacesAsync(string category, double latitude, double longitude, int radius);
        Task<PlaceDetailResult> GetPlaceDetailAndReviews(string placeId, double? latitude, double? longitude);
    }
}
