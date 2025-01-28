namespace yummyApp.Application.Dtos.GoogleApis
{
    public class Review
    {
        public string Author_Name { get; set; } // Yorumu yazan kişinin adı
        public string Author_Url { get; set; }  // Yazarın profil URL'si (varsa)
        public string Original_Language { get; set; }   // Yorumun dili
        public string Profile_Photo_Url { get; set; }   // Yorumun dili
        public string Text { get; set; }       // Yorumun içeriği
        public int Rating { get; set; }        // Yorumun verdiği puan
        public int Time { get; set; }          // Yorumun zaman damgası (Unix Epoch Time formatında)
    }
    public class PlaceDetailsResult
    {
        public PlaceDetails Result { get; set; } // İşletmeye ait detaylar
    }
    public class PlaceDetails
    {
        public List<Review> Reviews { get; set; } // İşletmeye ait yorumlar
    }
}
