using System.Linq.Expressions;

namespace HotelApp.Data.Interfaces
{
    public interface IReservationRepository<T> where T : class
    {
        Task<IList<T>> GetAllReservationsWithTheirCustomers(CancellationToken cancellationToken = default(CancellationToken));
    }
}
