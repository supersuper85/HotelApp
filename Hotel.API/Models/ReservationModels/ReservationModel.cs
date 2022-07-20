using HotelApp.API.Models.CustomerModels;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.Models.ReservationModels
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ApartmentId { get; set; }
        public int HotelId { get; set; }
        public CustomerModel Customer { get; set; }
        

    }
}
