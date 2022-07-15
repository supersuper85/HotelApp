using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.InputModels
{
    public class ReservationInputModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The number of accommodation days cannot be 0!!")]
        public int NumberOfDays { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id-ul apartamentului inchiriat nu poate fi 0!")]
        public int ApartmentId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The id of the hotel to which the rented apartment belongs cannot be 0!")]
        public int HotelId { get; set; }
        [Required]
        public CustomerInputModel Customer { get; set; }
        
    }
}
