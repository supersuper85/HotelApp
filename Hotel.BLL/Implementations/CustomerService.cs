using AutoMapper;
using HotelApp.BLL.Constants;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Extensions.Audit;
using HotelApp.BLL.Interfaces;
using HotelApp.BLL.Models.AuditModels;
using HotelApp.BLL.Validations;
using HotelApp.Data.Entities;
using HotelApp.Data.Interfaces;
using System.Net.Http.Json;

namespace HotelApp.BLL.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public CustomerService(ICustomerRepository customerRepository,
            IReservationRepository reservationRepository,
            IMapper mapper,
            HttpClient httpClient)
        {
            _reservationRepository = reservationRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _httpClient = httpClient;
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
            var customerValidator = new CustomerDatabaseValidator(_reservationRepository, _customerRepository, null, null);
            await customerValidator.CheckCustomerPostModel(model);

            var newCustomer = _mapper.Map<CustomerDto, Customer>(model);

            var auditSender = new AuditSender<Customer>(_httpClient);

            var addedCustomer = await _customerRepository.AddAsync(newCustomer);

            var operationIsSuccesfully = addedCustomer != null;
            if (operationIsSuccesfully)
                auditSender.ReportPostRequest(newCustomer);

            return _mapper.Map<CustomerDto>(addedCustomer);

        }
        public async Task<bool> Edit(CustomerDto model)
        {
            var oldValueOf_Customer = await _customerRepository.GetCustomerByIdAsNoTracking(model.Id);

            var customerValidator = new CustomerDatabaseValidator(_reservationRepository, _customerRepository, null, null);
            await customerValidator.CheckCustomerPutModel(model, oldValueOf_Customer);

            var newValueOf_Customer = _mapper.Map<CustomerDto, Customer>(model);

            var auditSender = new AuditSender<Customer>(_httpClient);

            var operationIsSuccesfully = await _customerRepository.UpdateAsync(newValueOf_Customer);

            if(operationIsSuccesfully)
                auditSender.ReportPutRequest(oldValueOf_Customer, newValueOf_Customer);

            return operationIsSuccesfully;
        }

        public async Task<CustomerDto> Delete(int id)
        {
            var customerValidator = new CustomerDatabaseValidator(_reservationRepository, _customerRepository, null, null);
            await customerValidator.CheckCustomerDeleteModel(id);

            var oldValueOf_Customer = await _customerRepository.SingleOrDefaultAsync(x => x.Id == id);

            var auditSender = new AuditSender<Customer>(_httpClient);

            var operationIsSuccesfully = await _customerRepository.DeleteAsync(oldValueOf_Customer);

            if (operationIsSuccesfully)
                auditSender.ReportDeleteRequest(oldValueOf_Customer);

            return operationIsSuccesfully ? _mapper.Map<CustomerDto>(oldValueOf_Customer) : null;

        }
    }
}
