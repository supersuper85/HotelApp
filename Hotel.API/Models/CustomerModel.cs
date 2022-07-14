using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }

        [Required]
        [Range(18, int.MaxValue, ErrorMessage = "People under the age of 18 cannot rent a room!")]
        public int Age { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "The name cannot be longer than 100 characters and less than 3 characters")]
        public string Name { get; set; }

        [Required]
        public int ApartmentId { get; set; }
        public int ReservationId { get; set; }
        public int HotelId { get; set; }
    }
}
