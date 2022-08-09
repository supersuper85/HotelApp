using HotelApp.Data.Entities;
using System.Linq.Expressions;

namespace HotelApp.Data.Interfaces
{
    public interface IApartmentRepository : IRepository<Apartment>
    {
        Task<IList<Apartment>> GetAllAvailableApartments(CancellationToken cancellationToken = default(CancellationToken));
        Task<Apartment> GetApartmentById(int id);
    }
}
