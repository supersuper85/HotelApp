using FsCheck;
using HotelApp.Data.Entities;
using HotelApp.Data.Entities.Context;
using HotelApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace HotelApp.Data.Implementations
{
    public class CustomerRepository : DefaultRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataBaseContext context) : base(context)
        {

        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var result = await _entities.SingleOrDefaultAsync(x => x.Id == id);
            return result;
        }
        public async Task<Customer> GetCustomerByIdAsNoTracking(int id)
        {
            var result = await _entities.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
