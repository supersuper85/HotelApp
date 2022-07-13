﻿namespace HotelApp.API.Models
{
    public class HotelModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ApartmentModel> Apartments { get; set; }
        public List<ReservationModel> Reservations { get; set; }
    }
}
