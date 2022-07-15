using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.InputModels
{
    public class HotelInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The name cannot be longer than 100 characters and less than 3 characters")]
        public string Name { get; set; }
        public List<ApartmentInputModel> Apartments { get; set; }
        public List<CustomerInputModel> Customers { get; set; }
        public List<ReservationInputModel> Reservations { get; set; }
    }
}
