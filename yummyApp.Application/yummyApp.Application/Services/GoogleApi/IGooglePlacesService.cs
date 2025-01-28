using yummyApp.Application.Dtos.GoogleApis;

namespace yummyApp.Application.Services.GoogleApi
{
    public interface IGooglePlacesService
    {
        Task<PlaceSearchResult> GetNearbyPlacesAsync(double latitude, double longitude, int radius = 15000);
        Task<List<Review>> GetPlaceReviews(string placeId);
    }
}
