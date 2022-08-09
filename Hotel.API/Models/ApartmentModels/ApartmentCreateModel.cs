namespace HotelApp.API.Models.ApartmentModels
{
    public class ApartmentCreateModel
    {
        public float DailyRentInEuro { get; set; }
        public int NumberOfRooms { get; set; }
        public int ApartmentNumber { get; set; }
        public int HotelId { get; set; }
    }
}
