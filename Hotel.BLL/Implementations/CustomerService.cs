using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Interfaces;
using HotelApp.Data.Entities;
using HotelApp.Data.Interfaces;

namespace HotelApp.BLL.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public async Task<IList<CustomerDto>> GetAll()
        {
            var customers = await _customerRepository.GetAllAsync();
            return _mapper.Map<IList<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> Get(int id)
        {
            var customerById = await _customerRepository.GetCustomerById(id);
            var mappedCustomer = _mapper.Map<CustomerDto>(customerById);

            return mappedCustomer;
        }

        public async Task<CustomerDto> Add(CustomerDto model)
        {
            var mappedCustomer = _mapper.Map<CustomerDto, Customer>(model);
            var addedCustomer = await _customerRepository.AddAsync(mappedCustomer);

            return _mapper.Map<CustomerDto>(addedCustomer);

        }
        public async Task<bool> Edit(CustomerDto model)
        {
            if (await _customerRepository.ExistsAsync(x => x.Id == model.Id))
            {
                var mappedCustomer = _mapper.Map<CustomerDto, Customer>(model);
                var response = await _customerRepository.UpdateAsync(mappedCustomer);

                return response;
            }
            return false;
        }

        public async Task<CustomerDto> Delete(int id)
        {
            if (await _customerRepository.ExistsAsync(x => x.Id == id))
            {
                var customer = await _customerRepository.SingleOrDefaultAsync(x => x.Id == id);

                var response = await _customerRepository.DeleteAsync(customer);
                return response ? _mapper.Map<CustomerDto>(customer) : null;

            }
            return null;
        }
    }
}
