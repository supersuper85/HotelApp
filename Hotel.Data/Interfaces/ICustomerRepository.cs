using HotelApp.Data.Entities;
using System.Linq.Expressions;

namespace HotelApp.Data.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomerById(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
