using HotelApp.Data.Entities;
using HotelApp.Data.Entities.Context;
using HotelApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.Data.Implementations
{
    public class ApartmentRepository<T> : BaseEntityFrameworkRepository<T>, IApartmentRepository<T>,  IRepository<T> where T : Apartment
    {
        protected readonly DbSet<T> _entities;
        protected readonly DataBaseContext Context;


        public ApartmentRepository(DataBaseContext context) : base(context)
        {
            Context = context;
            _entities = context.Set<T>();
        }
        public async Task<IList<T>> GetAllAvailableHotelRooms(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _entities.Where(s => s.ReservationId == 0).ToListAsync(cancellationToken);
        }
    }
    
}
