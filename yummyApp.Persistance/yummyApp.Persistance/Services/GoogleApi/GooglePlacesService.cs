using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using yummyApp.Application.Dtos.GoogleApis;
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

        public async Task<PlaceSearchResult> GetNearbyPlaces(double latitude, double longitude, int radius = 1500)
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
                    return places;
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
    }
}
