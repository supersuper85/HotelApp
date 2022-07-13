using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.BLL.Dto
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ApartmentDto> Apartments { get; set; }
        public List<ReservationDto> Reservations { get; set; }
    }
}
