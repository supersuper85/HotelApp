namespace HotelApp.Data.Entities
{
    public class Apartment
    {
        public int Id { get; set; }
        public float DailyRentInEuro { get; set; }
        public int NumberOfRooms { get; set; }
        public int RoomNumber { get; set; }
        public int ReservationId { get; set; }
        public int HotelId { get; set; }
    }
}
