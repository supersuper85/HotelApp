using HotelApp.Data.Interfaces;

namespace HotelApp.Data.Entities 
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Apartment> Apartments { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
