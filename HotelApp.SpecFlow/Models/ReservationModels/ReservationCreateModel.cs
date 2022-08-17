using HotelApp.SpecFlow.Models.CustomerModels;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.SpecFlow.Models.ReservationModels
{
    public class ReservationCreateModel
    {
        public int NumberOfDays { get; set; }
        public int ApartmentId { get; set; }
        public int HotelId { get; set; }
        public ReservationCustomerCreateModel Customer { get; set; }
    }
}
