using HotelApp.BLL.Dto;
using HotelApp.BLL.Exceptions;
using HotelApp.Data.Entities;
using HotelApp.Data.Interfaces;

namespace HotelApp.BLL.Validations
{
    public class CustomerDatabaseValidator
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IRepository<Hotel> _hotelRepository;
        private readonly ICustomerRepository _customerRepository;

        public CustomerDatabaseValidator(IReservationRepository reservationRepository, ICustomerRepository customerRepository, IApartmentRepository apartmentRepository, IRepository<Hotel> hotelRepository)
        {
            _reservationRepository = reservationRepository;
            _apartmentRepository = apartmentRepository;
            _hotelRepository = hotelRepository;
            _customerRepository = customerRepository;
        }
        public async Task CheckCustomerPostModel(CustomerDto model)
        {
            await CheckCustomerCNP(model);
        }

        public async Task CheckCustomerCNP(CustomerDto model)
        {
            if (await _customerRepository.ExistsAsync(x => x.CNP == model.CNP))
            {
                throw new DatabaseValidatorException("The entered CNP already exists in the system!");
            }
        }


        public async Task CheckCustomerPutModel(CustomerDto model)
        {
            await CheckCustomerExists(model.Id);
            await CheckCustomerCNPIsModifiedOrAvailable(model);
        }
        public async Task CheckCustomerExists(int id)
        {
            if (!await _customerRepository.ExistsAsync(x => x.Id == id))
            {
                throw new DatabaseValidatorException("The entered id does not correspond to any customer!");
            }
        }
        public async Task CheckCustomerCNPIsModifiedOrAvailable(CustomerDto model)
        {
            if ((await _customerRepository.ExistsAsync(x => x.CNP == model.CNP) && !await _customerRepository.ExistsAsync(x => x.CNP == model.CNP && x.Id == model.Id)))
            {
                throw new DatabaseValidatorException("The entered CNP already exists in the system!");
            }
        }


        public async Task CheckCustomerDeleteModel(int id)
        {
            await CheckCustomerExists(id);
            await CheckCustomerHaveReservations(id);
        }
        public async Task CheckCustomerHaveReservations(int id)
        {
            if (await _reservationRepository.ExistsAsync(x => x.CustomerId == id))
            {
                throw new DatabaseValidatorException("The entered customer has active reservations!");
            }
        }
    }
}
