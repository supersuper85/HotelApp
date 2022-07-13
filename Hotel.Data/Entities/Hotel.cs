using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
