using System.Linq.Expressions;

namespace HotelApp.Data.Interfaces
{
    public interface IApartmentRepository<T> where T : class
    {
        Task<IList<T>> GetAllAvailableHotelRooms(CancellationToken cancellationToken = default(CancellationToken));
    }
}
