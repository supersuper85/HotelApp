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
        public float DailyRent { get; set; }
        public int DailyRentInEuro { get; set; }
        public int RoomNumber { get; set; }
        public int ReservationId { get; set; }
        public int HotelId { get; set; }
    }
}
