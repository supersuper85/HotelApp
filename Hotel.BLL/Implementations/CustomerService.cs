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
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IList<CustomerDto>>(entities);
        }

        public async Task<CustomerDto> Get(int id)
        {
            var entities = await _repository.GetAllAsync();
            var entityById = entities.SingleOrDefault(e => e.Id == id);
            var mapped = _mapper.Map<CustomerDto>(entityById);

            return mapped;
        }

        public async Task<CustomerDto> Add(CustomerDto model)
        {
            //verify if borrower exists in database!!!
            var mappedBook = _mapper.Map<CustomerDto, Customer>(model);
            var addedBook = await _repository.AddAsync(mappedBook);

            return _mapper.Map<CustomerDto>(addedBook);

        }
        public async Task<bool> Edit(CustomerDto model)
        {
            if (await _repository.ExistsAsync(x => x.Id == model.Id))
            {
                var mappedBook = _mapper.Map<CustomerDto, Customer>(model);
                var response = await _repository.UpdateAsync(mappedBook);

                return response;
            }
            return false;
        }

        public async Task<CustomerDto> Delete(int id)
        {
            if (await _repository.ExistsAsync(x => x.Id == id))
            {
                var book = await _repository.SingleOrDefaultAsync(x => x.Id == id);

                var response = await _repository.DeleteAsync(book);
                return response ? _mapper.Map<CustomerDto>(book) : null;

            }
            return null;
        }
    }
}
