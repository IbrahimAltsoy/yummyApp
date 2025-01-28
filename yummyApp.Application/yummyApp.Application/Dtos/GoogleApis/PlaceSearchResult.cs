namespace yummyApp.Application.Dtos.GoogleApis
{
    public class PlaceSearchResult
    {
        public List<Place> Results { get; set; }
    }
    public class Place
    {
        public string Name { get; set; }  // JSON: "name"
        public string Vicinity { get; set; }  // JSON: "vicinity"
        public double Rating { get; set; }  // JSON: "rating"
        public string Place_Id { get; set; }  // JSON: "place_id"
        public List<Photo> Photos { get; set; }  // JSON: "photos"
        public string PhotoUrl { get; set; } // İlk fotoğrafın tam URL'si
        public OpeningHours Opening_Hours { get; set; }  // JSON: "opening_hours"
        public string Distance { get; set; }  // Hesaplanan mesafe
        public int User_Ratings_Total { get; set; }  // JSON: "user_ratings_total"
    }

    public class Photo
    {
        public string Photo_Reference { get; set; }  // JSON: "photo_reference"
        public int Width { get; set; }  // JSON: "width"
        public int Height { get; set; }  // JSON: "height"
        public List<string> Html_Attributions { get; set; }
        
    }
    //public class Html_Attributions
    //{

    //}

    public class OpeningHours
    {
        public bool Open_Now { get; set; }  // JSON: "open_now"
    }
}
