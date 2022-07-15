using HotelApp.Data.Entities;
using HotelApp.Data.Entities.Context;
using HotelApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.Data.Implementations
{
    public class ReservationRepository<T> : BaseEntityFrameworkRepository<T>, IReservationRepository<T>, IRepository<T> where T : Reservation
    {
        protected readonly DbSet<T> _entities;
        protected readonly DataBaseContext Context;

        public ReservationRepository(DataBaseContext context) : base(context)
        {
            Context = context;
            _entities = context.Set<T>();
        }
        public async Task<IList<T>> GetAllReservationsWithTheirCustomers(CancellationToken cancellationToken = default)
        {
            return await _entities.Include("Customer").ToListAsync(cancellationToken);
        }
    }
}
