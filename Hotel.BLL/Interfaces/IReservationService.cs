using HotelApp.BLL.Dto;


namespace HotelApp.BLL.Interfaces
{
    public interface IReservationService
    {
        Task<IList<ReservationDto>> GetAll();
        Task<IList<ReservationDto>> GetAllReservationsWithTheirCustomers();
        Task<ReservationDto> GetAReservationById(int id);
        Task<ReservationDto> Add(ReservationDto model);
        Task<bool> EditAReservation(ReservationDto model);
        Task<ReservationDto> Delete(int id);
    }
}
