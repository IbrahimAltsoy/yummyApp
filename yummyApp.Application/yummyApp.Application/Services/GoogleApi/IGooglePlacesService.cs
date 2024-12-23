using yummyApp.Application.Dtos.GoogleApis;

namespace yummyApp.Application.Services.GoogleApi
{
    public interface IGooglePlacesService
    {
        Task<PlaceSearchResult> GetNearbyPlaces(double latitude, double longitude, int radius = 1500);
    }
}
