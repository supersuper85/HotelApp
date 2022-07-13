﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.BLL.Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; } 
        public int ApartmentId { get; set; }
        public virtual ApartmentDto Apartment { get; set; }
        public int ReservationId { get; set; }
        public virtual ReservationDto Reservation { get; set; }
    }
}