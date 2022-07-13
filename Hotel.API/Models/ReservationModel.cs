namespace HotelApp.API.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public ApartmentModel Apartment { get; set; }
        public int CustomerId { get; set; }
        public CustomerModel Customer { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int HotelId { get; set; }
        
    }
}
