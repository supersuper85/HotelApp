using HotelApp.Data.Entities;
using HotelApp.Data.Entities.Context;
using HotelApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelApp.Data.Implementations
{
    public class CustomerRepository : BaseEntityFrameworkRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataBaseContext context) : base(context)
        {
        }

        public async Task<Customer> GetCustomerById(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _entities.SingleOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
