﻿using System.ComponentModel.DataAnnotations;

namespace HotelApp.API.Models.ReservationModels
{
    public class ReservationEditModel
    {
        public int Id { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ApartmentId { get; set; }
    }
}
