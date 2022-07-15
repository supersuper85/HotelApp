using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.Models
{
    public class ApartmentModel
    {
        public int Id { get; set; }
        public float DailyRentInEuro { get; set; }
        public int NumberOfRooms { get; set; }
        public int RoomNumber { get; set; }

        public int CustomerId { get; set; }
        public int ReservationId { get; set; }
        public int HotelId { get; set; }
    }
}
