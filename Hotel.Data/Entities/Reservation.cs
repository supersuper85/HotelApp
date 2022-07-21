namespace HotelApp.Data.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int ApartmentId { get; set; }
        public Customer Customer { get; set; }
        public int HotelId { get; set; }
    }
}
