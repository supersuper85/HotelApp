using HotelApp.BLL.Dto;

namespace HotelApp.BLL.Interfaces
{
    public interface ICustomerService
    {
        Task<IList<CustomerDto>> GetAll();
        Task<CustomerDto> Get(int id);
        Task<CustomerDto> Add(CustomerDto model);
        Task<bool> Edit(CustomerDto model);
        Task<CustomerDto> Delete(int id);
    }
}
