using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.BLL.Dto
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int ApartmentId { get; set; }
        public CustomerDto Customer { get; set; }
        public int HotelId { get; set; }

        public int CustomerId { get; set; }
    }
}
