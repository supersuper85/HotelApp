using HotelApp.Data.Entities;

namespace HotelApp.Data.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<IList<Reservation>> GetAllReservations();
        Task<Reservation> GetReservationById(int id);
    }
}
