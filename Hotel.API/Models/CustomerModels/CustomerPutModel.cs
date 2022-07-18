using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.Models.CustomerModels
{
    public class CustomerPutModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Customer Id can not be 0!")]
        public int Id { get; set; }
        [Required]
        [Range(18, int.MaxValue, ErrorMessage = "People under the age of 18 cannot rent a room!")]
        public int Age { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "The name cannot be longer than 100 characters and less than 3 characters")]
        public string Name { get; set; }

        [StringLength(13, MinimumLength = 13, ErrorMessage = "The CNP must have 13 characters!")]
        public string CNP { get; set; }
    }
}
