using HotelApp.API.Models.CustomerModels;

namespace HotelApp.API.Models.ReservationModels
{
    public class ReservationDeleteModel
    {
        public int Id { get; set; }
        public CustomerDeleteModel Customer { get; set; }
    }
}
