namespace yummyApp.Domain.Enums
{
    public static class PlaceCategories
    {
        // **GENEL KATEGORİLER**
        public enum General
        {
            Restaurant,
            Cafe,
            Bar,
            Supermarket,
            ShoppingMall
        }

        // **HİZMETLER (BANKA, HASTANE VB.)**
        public enum Services
        {
            Bank,
            ATM,
            Hospital,
            Pharmacy,
            Police,
            PostOffice
        }

        // **EĞLENCE & SOSYAL MEKANLAR**
        public enum Entertainment
        {
            Park,
            Museum,
            MovieTheater,
            NightClub,
            Stadium
        }

        // **ULAŞIM & SEYAHAT**
        public enum Travel
        {
            TrainStation,
            BusStation,
            TaxiStand,
            Airport,
            CarRental,
            Parking
        }
        public enum Other
        {
            NEri, NEriii, EsrarShop
        }
    }

}
