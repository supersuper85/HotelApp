namespace HotelApp.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public int ApartmentId { get; set; }
        public int ReservationId { get; set; }
        public int HotelId { get; set; }
    }
}
