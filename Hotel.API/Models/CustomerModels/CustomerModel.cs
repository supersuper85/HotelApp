using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.Models.CustomerModels
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string CNP { get; set; }

        public int ApartmentId { get; set; }
        public int ReservationId { get; set; }
        public int HotelId { get; set; }
    }
}
