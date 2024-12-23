namespace yummyApp.Application.Dtos.GoogleApis
{
    public class PlaceSearchResult
    {
        public List<Place> Results { get; set; }
    }
    public class Place
    {
        public string Name { get; set; }
        public string Vicinity { get; set; }  // Address or vicinity of the place
        public double Rating { get; set; }    // Rating of the place
        public string PlaceId { get; set; }
    }
}
