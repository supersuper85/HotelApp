using HotelApp.Data.Entities;
using HotelApp.Data.Entities.Context;
using HotelApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelApp.Data.Implementations
{
    public class ReservationRepository :  BaseEntityFrameworkRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(DataBaseContext context) : base(context)
        {
        }

        public async Task<IList<Reservation>> GetAllReservations()
        {
            var allReservations = await _entities.Include(x=>x.Customer).ToListAsync();
            return allReservations;
        }

        public async Task<Reservation> GetReservationById(int id)
        {
            var result = await _entities.Include(x => x.Customer).SingleOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
