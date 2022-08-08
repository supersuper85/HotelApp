using HotelApp.Data.Entities;
using System.Linq.Expressions;

namespace HotelApp.Data.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomerById(int id);
        Task<Customer> GetCustomerByIdAsNoTracking(int id);
    }
}
