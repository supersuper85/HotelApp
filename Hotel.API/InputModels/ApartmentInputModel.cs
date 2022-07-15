using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.InputModels
{
    public class ApartmentInputModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The price of the apartment can not be 0!")]
        public float DailyRentInEuro { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The number of rooms in an apartment cannot be 0!")]
        public int NumberOfRooms { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The apartment number cannot be 0!")]
        public int RoomNumber { get; set; }

        public int CustomerId { get; set; }
        public int ReservationId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The apartment number cannot be 0!")]
        public int HotelId { get; set; }
    }
}
