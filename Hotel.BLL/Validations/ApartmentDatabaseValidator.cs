using HotelApp.BLL.Dto;
using HotelApp.BLL.Exceptions;
using HotelApp.Data.Entities;
using HotelApp.Data.Interfaces;

namespace HotelApp.BLL.Validations
{
    public class ApartmentDatabaseValidator
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IRepository<Hotel> _hotelRepository;
        private readonly ICustomerRepository _customerRepository;

        public ApartmentDatabaseValidator(IReservationRepository reservationRepository, ICustomerRepository customerRepository, IApartmentRepository apartmentRepository, IRepository<Hotel> hotelRepository)
        {
            _reservationRepository = reservationRepository;
            _apartmentRepository = apartmentRepository;
            _hotelRepository = hotelRepository;
            _customerRepository = customerRepository;
        }
        public async Task CheckApartmentPostModel(ApartmentDto model)
        {
            await CheckApartmentNumber(model);
            await CheckHotelExists(model);
        }
        private async Task CheckApartmentNumber(ApartmentDto model)
        {
            if (await _apartmentRepository.ExistsAsync(x => x.ApartmentNumber == model.ApartmentNumber))
            {
                throw new DatabaseValidatorException("The apartment number is already used!");
            }
        }
        private async Task CheckHotelExists(ApartmentDto model)
        {
            if (!await _apartmentRepository.ExistsAsync(x => x.HotelId == model.HotelId))
            {
                throw new DatabaseValidatorException("The entered hotel ID is not valid!");
            }
        }
        public async Task CheckApartmentPutModel(Apartment entity, ApartmentDto model)
        {
            CheckApartmentExistsByEntity(entity);
            await CheckApartmentNumberByEntity(entity, model);
        }
        private void CheckApartmentExistsByEntity(Apartment entity)
        {
            if (entity == null)
            {
                throw new DatabaseValidatorException("The entered apartment ID is not valid!");
            }
        }
        private async Task CheckApartmentNumberByEntity(Apartment entity, ApartmentDto model)
        {
            if (model.ApartmentNumber != entity.ApartmentNumber)
            {
                await CheckApartmentNumber(model);
            }
        }

        public async Task CheckApartmentDeleteModel(Apartment model)
        {
            CheckApartmentExistsByEntity(model);
            CheckApartmentHaveActiveReservationByEntity(model);
        }
        private void CheckApartmentHaveActiveReservationByEntity(Apartment model)
        {
            if (model.ReservationId != 0)
            {
                throw new DatabaseValidatorException("The entered apartment has an active reservation.");
            }
        }

    }
}
