using HotelApp.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.BLL.Interfaces
{
    public interface IReservationService
    {
        Task<IList<ReservationDto>> GetAll();
        Task<IList<ReservationDto>> GetAllReservationsWithTheirCustomers();
        Task<ReservationDto> GetAReservationWithHisCustomer(int id);
        Task<ReservationDto> Add(ReservationDto model);
        Task<bool> EditAReservation(ReservationDto model);
        Task<ReservationDto> Delete(int id);
    }
}
