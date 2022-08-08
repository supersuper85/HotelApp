using HotelApp.Data.Interfaces;

namespace HotelApp.Data.Entities
{
    public class Apartment
    {
        public int Id { get; set; }
        public float DailyRentInEuro { get; set; }
        public float NumberOfRooms { get; set; }
        public int ApartmentNumber { get; set; }

        public int CustomerId { get; set; }
        public int ReservationId { get; set; }
        public int HotelId { get; set; }
    }
}
