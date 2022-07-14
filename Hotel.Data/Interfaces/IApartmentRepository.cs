using System.Linq.Expressions;

namespace HotelApp.Data.Interfaces
{
    public interface IApartmentRepository<T> where T : class
    {
        Task<IList<T>> GetAllAvailableApartments(CancellationToken cancellationToken = default(CancellationToken));
    }
}
