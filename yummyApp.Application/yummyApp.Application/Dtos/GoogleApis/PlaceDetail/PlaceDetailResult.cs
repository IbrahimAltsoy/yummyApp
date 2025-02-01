namespace yummyApp.Application.Dtos.GoogleApis.PlaceDetail
{
    public class PlaceDetailResult
    {
        public Place Result { get; set; }
    }
    public class Place
    {
        public string Name { get; set; } 
        public string Vicinity { get; set; } 
        public double Rating { get; set; } 
        public int User_Ratings_Total { get; set; } 
        public string Place_Id { get; set; }
        public List<Photo> Photos { get; set; }  
        public List<string> PhotoUrls { get; set; } 
        public OpeningHours Opening_Hours { get; set; }
        public Geometry Geometry { get; set; }
        public double Distance { get; set; } 

        // Yeni Eklenen Alanlar
        public string Formatted_Phone_Number { get; set; } 
        public string Website { get; set; }
        public List<string> Weekday_Text { get; set; }
        public List<Review> Reviews { get; set; } 
        public List<string> Services { get; set; } 
        public List<string> Menu { get; set; }
        public SocialMedia Social_Media { get; set; } 
    }

    public class Photo
    {
        public string Photo_Reference { get; set; }
        public int Width { get; set; }
        public int Height { get; set; } 
        public List<string> Html_Attributions { get; set; }
    }

    public class Geometry
    {
        public Location Location { get; set; }
    }

    public class Location
    {
        public double Lat { get; set; } 
        public double Lng { get; set; } 
    }

    public class OpeningHours
    {
        public bool Open_Now { get; set; } 
        public List<string> Weekday_Text { get; set; }  
    }

    public class Review
    {
        public string Author_Name { get; set; }  
        public double Rating { get; set; }  
        public string Text { get; set; }  
        public string Relative_Time_Description { get; set; }  
    }

    public class SocialMedia
    {
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
    }

}
