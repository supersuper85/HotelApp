using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.BLL.Dto
{
    public class ApartmentDto
    {
        public int Id { get; set; }
        public float DailyRentInEuro { get; set; }
        public int NumberOfRooms { get; set; }
        public int ApartmentNumber { get; set; }

        public int CustomerId { get; set; }
        public int ReservationId { get; set; }
        public int HotelId { get; set; }
    }
}
