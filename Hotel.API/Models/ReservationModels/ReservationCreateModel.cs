using HotelApp.API.Models.CustomerModels;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.Models.ReservationModels
{
    public class ReservationCreateModel
    {
        public int NumberOfDays { get; set; }
        public int ApartmentId { get; set; }
        public int HotelId { get; set; }
        public CustomerCreateModel Customer { get; set; }
    }
}
