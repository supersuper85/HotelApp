using System.ComponentModel.DataAnnotations;

namespace HotelApp.SpecFlow.Models.ApartmentModels
{
    public class ApartmentModel
    {
        public int Id { get; set; }
        public float DailyRentInEuro { get; set; }
        public int NumberOfRooms { get; set; }
        public int ApartmentNumber { get; set; }

        public int CustomerId { get; set; }
        public int ReservationId { get; set; }
        public int HotelId { get; set; }
    }
}
