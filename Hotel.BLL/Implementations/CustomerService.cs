using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Interfaces;
using HotelApp.Data.Entities;
using HotelApp.Data.Interfaces;

namespace HotelApp.BLL.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _repository;
        private readonly IMapper _mapper;

        public CustomerService(IRepository<Customer> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IList<CustomerDto>> GetAll()
        {
            var customers = await _repository.GetAllAsync();
            return _mapper.Map<IList<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> Get(int id)
        {
            var customers = await _repository.GetAllAsync();
            var customerById = customers.SingleOrDefault(e => e.Id == id);
            var mappedCustomer = _mapper.Map<CustomerDto>(customerById);

            return mappedCustomer;
        }

        public async Task<CustomerDto> Add(CustomerDto model)
        {
            var mappedCustomer = _mapper.Map<CustomerDto, Customer>(model);
            var addedCustomer = await _repository.AddAsync(mappedCustomer);

            return _mapper.Map<CustomerDto>(addedCustomer);

        }
        public async Task<bool> Edit(CustomerDto model)
        {
            if (await _repository.ExistsAsync(x => x.Id == model.Id))
            {
                var mappedCustomer = _mapper.Map<CustomerDto, Customer>(model);
                var response = await _repository.UpdateAsync(mappedCustomer);

                return response;
            }
            return false;
        }

        public async Task<CustomerDto> Delete(int id)
        {
            if (await _repository.ExistsAsync(x => x.Id == id))
            {
                var customer = await _repository.SingleOrDefaultAsync(x => x.Id == id);

                var response = await _repository.DeleteAsync(customer);
                return response ? _mapper.Map<CustomerDto>(customer) : null;

            }
            return null;
        }
    }
}
