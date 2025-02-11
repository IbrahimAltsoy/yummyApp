using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using yummyApp.Application.Dtos.GoogleApis;
using yummyApp.Application.Dtos.GoogleApis.PlaceDetail;
using yummyApp.Application.Services.GoogleApi;
using yummyApp.Domain.Enums;

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

        public async Task<PlaceSearchResult> GetNearbyPlacesAsync(string category,double latitude, double longitude, int radius)
        {           
            try
            {
                var client = new HttpClient();
                //List<string> placeTypes = GetPlaceTypesByCategory(category);
                //string selectedType = placeTypes.FirstOrDefault() ?? "restaurant";
                var url = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}&radius={radius}&type={category}&key={_apiKey}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var places = JsonConvert.DeserializeObject<PlaceSearchResult>(json);
                    foreach (var place in places.Results)
                    {
                        var firstPhoto = place.Photos?.FirstOrDefault();
                        place.PhotoUrl = firstPhoto != null
                            ? $"https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photo_reference={firstPhoto.Photo_Reference}&key={_apiKey}"
                            : null;

                        if (place.Geometry?.Location != null)
                        {
                            place.Distance = await GetGoogleMapsDistance(latitude, longitude, place.Geometry.Location.Lat, place.Geometry.Location.Lng);
                        }
                    }
                    //var filterData = places.Results.OrderBy(x=>x.User_Ratings_Total).ToList();
                    return places;
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    return null!;
                }
            }
            catch (Exception ex)
            {
                return null!;
            }
        }

        public async Task<PlaceDetailResult> GetPlaceReviews(string placeId, double? latitude, double? longitude)
        {
            try
            {
                var url = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={placeId}&fields=name,formatted_address,formatted_phone_number,website,opening_hours,geometry,rating,user_ratings_total,photos,vicinity,reviews&language=tr&key={_apiKey}";

                var client = new HttpClient();
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
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
                        detailsResult!.Result.Distance = await GetGoogleMapsDistance(latitude!.Value, longitude!.Value, placeLocation.Lat, placeLocation.Lng);
                    }
                    return detailsResult ?? new PlaceDetailResult();
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    return null!;
                }
            }
            catch (Exception ex)
            {
                return null!;
            }
        }       
        private async Task<double> GetGoogleMapsDistance(double userLat, double userLng, double placeLat, double placeLng)
        {

          
            string url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={userLat.ToString(CultureInfo.InvariantCulture)},{userLng.ToString(CultureInfo.InvariantCulture)}&destinations={placeLat.ToString(CultureInfo.InvariantCulture)},{placeLng.ToString(CultureInfo.InvariantCulture)}&mode=driving&key={_apiKey}";

            Console.WriteLine($"📍 Google Maps API URL: {url}");


            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(json);
                    double distanceMeters = result.rows[0].elements[0].distance.value;
                    double distanceKm = distanceMeters / 1000;
                    return distanceKm;
                }
            }
            return -1; 
        }


        private double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
        private List<string> GetPlaceTypesByCategory(string category)
        {
            return category.ToLower() switch
            {
                "foodDrink" => Enum.GetValues(typeof(PlaceCategories.FoodDrink))
                                 .Cast<PlaceCategories.FoodDrink>()
                                 .Select(e => e.ToString().ToLower())
                                 .ToList(),

                "healthCare" => Enum.GetValues(typeof(PlaceCategories.HealthCare))
                                  .Cast<PlaceCategories.HealthCare>()
                                  .Select(e => e.ToString().ToLower())
                                  .ToList(),

                "education" => Enum.GetValues(typeof(PlaceCategories.Education))
                                       .Cast<PlaceCategories.Education>()
                                       .Select(e => e.ToString().ToLower())
                                       .ToList(),

                "transportTravel" => Enum.GetValues(typeof(PlaceCategories.TransportTravel))
                                .Cast<PlaceCategories.TransportTravel>()
                                .Select(e => e.ToString().ToLower())
                                .ToList(),

                "shopping" => Enum.GetValues(typeof(PlaceCategories.Shopping))
               .Cast<PlaceCategories.Shopping>()
               .Select(e => e.ToString().ToLower())
               .ToList(),

                "publicServices" => Enum.GetValues(typeof(PlaceCategories.PublicServices))
                .Cast<PlaceCategories.PublicServices>()
                .Select(e => e.ToString().ToLower())
                .ToList(),

                "entertainmentLeisure" => Enum.GetValues(typeof(PlaceCategories.EntertainmentLeisure))
               .Cast<PlaceCategories.EntertainmentLeisure>()
               .Select(e => e.ToString().ToLower())
               .ToList(),


                "finance" => Enum.GetValues(typeof(PlaceCategories.Finance))
                .Cast<PlaceCategories.Finance>()
                .Select(e => e.ToString().ToLower())
                .ToList(),

                "services" => Enum.GetValues(typeof(PlaceCategories.Services))
               .Cast<PlaceCategories.Services>()
               .Select(e => e.ToString().ToLower())
               .ToList(),

                _ => new List<string> { "restaurant" } // ❗ Geçersiz kategori gelirse "restaurant" döndür
            };
        }

    }
}
//var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={lat1},{lon1}&destinations={lat2},{lon2}&key=YOUR_GOOGLE_API_KEY";
