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

            var mappedCustomer = _mapper.Map<CustomerDto, Customer>(model);

            var auditSender = new AuditSender();
            auditSender.SetInitialValues<Customer>(_httpClient, "Insert", null);

            var addedCustomer = await _customerRepository.AddAsync(mappedCustomer);

            var operationIsSuccesfully = addedCustomer != null;
            await SendAuditPostRequest<Customer>(auditSender, operationIsSuccesfully, addedCustomer);

            return _mapper.Map<CustomerDto>(addedCustomer);

        }
        public async Task<bool> Edit(CustomerDto model)
        {
            var customerEntity = await _customerRepository.GetCustomerByIdAsNoTracking(model.Id);

            var customerValidator = new CustomerDatabaseValidator(_reservationRepository, _customerRepository, null, null);
            await customerValidator.CheckCustomerPutModel(model, customerEntity);

            var mappedCustomer = _mapper.Map<CustomerDto, Customer>(model);

            var auditSender = new AuditSender();
            auditSender.SetInitialValues(_httpClient, "Update", customerEntity);

            var response = await _customerRepository.UpdateAsync(mappedCustomer);

            await SendAuditPostRequest(auditSender, response, mappedCustomer);

            return response;
        }

        public async Task<CustomerDto> Delete(int id)
        {
            var customerValidator = new CustomerDatabaseValidator(_reservationRepository, _customerRepository, null, null);
            await customerValidator.CheckCustomerDeleteModel(id);

            var customer = await _customerRepository.SingleOrDefaultAsync(x => x.Id == id);

            var auditSender = new AuditSender();
            auditSender.SetInitialValues<Customer>(_httpClient, "Delete", customer);

            var response = await _customerRepository.DeleteAsync(customer);

            await SendAuditPostRequest<Customer>(auditSender, response, null);

            return response ? _mapper.Map<CustomerDto>(customer) : null;

        }


        public async Task<AuditGetModel> SendAuditPostRequest<T>(AuditSender auditSender, bool operationIsSuccessfuly, T entity)  where T : class
        {
            try
            {
                if (operationIsSuccessfuly)
                {
                    auditSender.SetNewValues(entity);
                    await auditSender.SendPostRequest();
                }
            }
            catch
            {
                auditSender.ResetConfiguration();
            }

            return null;
        }
    }
}
