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

        private async Task CheckCustomerCNP(CustomerDto model)
        {
            if (await _customerRepository.ExistsAsync(x => x.CNP == model.CNP))
            {
                throw new DatabaseValidatorException("The entered CNP already exists in the system!");
            }
        }


        public async Task CheckCustomerPutModel(CustomerDto model, Customer entity)
        {
            CheckObjectPropertiesValueAreTheSame(model, entity);
            CheckEntityIsNotNull(entity);
            await CheckCustomerCNPIsModifiedOrAvailable(model);
        }
        private void CheckObjectPropertiesValueAreTheSame<T, U>(T self, U to) where T : class where U : class
        {
            var areEqual = true;
            if (self != null && to != null)
            {
                Type typeT = typeof(T);
                Type typeU = typeof(U);
                foreach (System.Reflection.PropertyInfo pi in typeT.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    object selfValue = typeT.GetProperty(pi.Name).GetValue(self, null);
                    object toValue = typeU.GetProperty(pi.Name).GetValue(to, null);

                    if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                    {
                        areEqual = false;
                        break;
                    }
                }
            }

            if (areEqual)
            {
                throw new DatabaseValidatorException("There is no difference between the entered model and the one in the database!");
            }
        }
        private void CheckEntityIsNotNull(Customer entity)
        {
            if (entity == null)
            {
                throw new DatabaseValidatorException("The entered id does not correspond to any customer!");
            }
        }
        
        private async Task CheckCustomerCNPIsModifiedOrAvailable(CustomerDto model)
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
        private async Task CheckCustomerExists(int id)
        {
            if (await _customerRepository.ExistsAsync(x => x.Id == id))
            {
                throw new DatabaseValidatorException("The entered id does not correspond to any customer!");
            }
        }
        private async Task CheckCustomerHaveReservations(int id)
        {
            if (await _reservationRepository.ExistsAsync(x => x.CustomerId == id))
            {
                throw new DatabaseValidatorException("The entered customer has active reservations!");
            }
        }
    }
}
