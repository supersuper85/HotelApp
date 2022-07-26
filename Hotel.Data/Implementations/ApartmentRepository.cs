using HotelApp.Data.Entities;
using HotelApp.Data.Entities.Context;
using HotelApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.Data.Implementations
{
    public class ApartmentRepository : BaseEntityFrameworkRepository<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(DataBaseContext context) : base(context)
        {
        }

        public async Task<IList<Apartment>> GetAllAvailableApartments(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _entities.Where(s => s.ReservationId == 0).ToListAsync(cancellationToken);
        }
    }
    
}
