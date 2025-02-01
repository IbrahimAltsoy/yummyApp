using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using yummyApp.Application.Dtos.GoogleApis;
using yummyApp.Application.Dtos.GoogleApis.PlaceDetail;
using yummyApp.Application.Services.GoogleApi;

namespace yummyApp.Persistance.Services.GoogleApi
{
    public class GooglePlacesService : IGooglePlacesService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        // IConfiguration ile API Key'i al
        public GooglePlacesService(IConfiguration configuration)
        {
            _apiKey = configuration["GoogleApi:ApiKey"]; // appsettings.json'dan API Key'i al
            _httpClient = new HttpClient();
        }

        public async Task<PlaceSearchResult> GetNearbyPlacesAsync(double latitude, double longitude, int radius = 15000)
        {

            // https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=38,4192,27,1287&radius=150000&key=AIzaSyDXEKGz7AjjAsE959dnItLbfmCju2v5LDU
            // https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=38.4192,27.1287&radius=1500&key=AIzaSyDXEKGz7AjjAsE959dnItLbfmCju2v5LDU
            try
            {
                var client = new HttpClient();
                var ap = _apiKey;

                var url = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}&radius={radius}&type=restaurant&key={_apiKey}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var places = JsonConvert.DeserializeObject<PlaceSearchResult>(json);
                    Parallel.ForEach(places.Results, place =>
                    {
                        var firstPhoto = place.Photos?.FirstOrDefault();
                        place.PhotoUrl = firstPhoto!= null
                            ? $"https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photo_reference={firstPhoto.Photo_Reference}&key={_apiKey}"
                            : null;
                        var placeLocation = place.Geometry?.Location;
                        if (placeLocation != null)
                        {
                            // Mesafeyi hesapla
                            place.Distance = CalculateDistance(latitude, longitude, placeLocation.Lat, placeLocation.Lng);
                        }
                    });

                    return places!;
                }
                else
                {
                    // Hata durumunda daha detaylı hata mesajı al
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Hata oluştu: {errorResponse}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
                return null;
            }
        }

        public async Task<PlaceDetailResult> GetPlaceReviews(string placeId, double? latitude, double? longitude)
        {
            try
            {
                // Place Details API URL'si
                // var url = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={placeId}&fields=reviews&key={_apiKey}";
                //var url = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={placeId}&fields=name,formatted_address,formatted_phone_number,website,opening_hours,geometry,rating,user_ratings_total,photos,reviews&key={_apiKey}";

                var url = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={placeId}&fields=name,formatted_address,formatted_phone_number,website,opening_hours,geometry,rating,user_ratings_total,photos,vicinity,reviews&language=tr&key={_apiKey}";

                var client = new HttpClient();
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    // API yanıtını modelle
                    var detailsResult = JsonConvert.DeserializeObject<PlaceDetailResult>(json);

                    if (detailsResult?.Result?.Photos != null)
                    {
                        detailsResult.Result.PhotoUrls = detailsResult.Result.Photos
                            .Select(p => $"https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photo_reference={p.Photo_Reference}&key={_apiKey}")
                            .ToList();
                    }
                    var placeLocation = detailsResult?.Result.Geometry?.Location;
                    if (placeLocation != null)
                    {
                        // Mesafeyi hesapla
                        detailsResult!.Result.Distance = CalculateDistance(latitude!.Value, longitude!.Value, placeLocation.Lat, placeLocation.Lng);
                    }
                    // Yorumları döndür

                    return detailsResult ?? new PlaceDetailResult();
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Hata oluştu: {errorResponse}");
                    return null!;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
                return null!;
            }
        }
        private double CalculateDistance(double userLat, double userLng, double placeLat, double placeLng)
        {
            const double EarthRadiusKm = 6371; // Dünya'nın yarıçapı (km cinsinden)

            double dLat = DegreesToRadians(placeLat - userLat);
            double dLng = DegreesToRadians(placeLng - userLng);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(userLat)) * Math.Cos(DegreesToRadians(placeLat)) *
                       Math.Sin(dLng / 2) * Math.Sin(dLng / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadiusKm * c; // Kilometre cinsinden mesafe
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

    }
}
