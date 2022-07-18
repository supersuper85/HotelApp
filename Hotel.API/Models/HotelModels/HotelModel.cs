using HotelApp.API.Models.ApartmentModels;
using HotelApp.API.Models.CustomerModels;
using HotelApp.API.Models.ReservationModels;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.Models.HotelModels
{
    public class HotelModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ApartmentModel> Apartments { get; set; }
        public List<CustomerModel> Customers { get; set; }
        public List<ReservationModel> Reservations { get; set; }
    }
}
