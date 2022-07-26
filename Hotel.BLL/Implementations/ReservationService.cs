using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Interfaces;
using HotelApp.BLL.Validations;
using HotelApp.Data.Entities;
using HotelApp.Data.Interfaces;

namespace HotelApp.BLL.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IRepository<Hotel> _hotelRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationService(
         IReservationRepository reservationRepository,
         ICustomerRepository customerRepository,
         IApartmentRepository apartmentRepository,
         IRepository<Hotel> defaultHotelRepository
        , IMapper mapper)
        {
            _customerRepository = customerRepository;
            _apartmentRepository = apartmentRepository;
            _hotelRepository = defaultHotelRepository;
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<IList<ReservationDto>> GetAll()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            return _mapper.Map<IList<ReservationDto>>(reservations);
        }

        public async Task<IList<ReservationDto>> GetAllReservationsWithTheirCustomers()
        {
            var reservations = await _reservationRepository.GetAllReservations();
            var mappedResult = _mapper.Map<IList<ReservationDto>>(reservations);
            return mappedResult;
        }


        public async Task<ReservationDto> GetAReservationById(int id)
        {
            var reservation = await _reservationRepository.GetReservationById(id);

            var result = _mapper.Map<ReservationDto>(reservation);
            return result;
        }

        public async Task<ReservationDto> Add(ReservationDto model)
        {
            var reservationValidator = new ReservationDatabaseValidator(_reservationRepository, _customerRepository, _apartmentRepository, _hotelRepository);
            await reservationValidator.CheckReservationPostModel(model);

            var mappedReservation = _mapper.Map<ReservationDto, Reservation>(model);

            var customerId = model.Customer.Id;
            mappedReservation.Customer = null;

            var addedReservation = await _reservationRepository.AddAsync(mappedReservation);


            var reservedApartment = await _apartmentRepository.SingleOrDefaultAsync(x => x.Id == model.ApartmentId);

            reservedApartment.ReservationId = addedReservation.Id;
            reservedApartment.CustomerId = customerId;

            var modifingApartmentResponse = await _apartmentRepository.UpdateAsync(reservedApartment);


            var customer = await _customerRepository.SingleOrDefaultAsync(x => x.Id == customerId);

/*            customer.ApartmentId = model.ApartmentId;
            customer.HotelId = model.HotelId;
            customer.ReservationId = addedReservation.Id;*/

            var modifingCustomerResponse = await _customerRepository.UpdateAsync(customer);

            return modifingApartmentResponse && modifingCustomerResponse ? _mapper.Map<ReservationDto>(addedReservation) : null;


        }
        public async Task<bool> EditAReservation(ReservationDto model)
        {
            var reservation = await _reservationRepository.GetReservationById(model.Id);

            var reservationValidator = new ReservationDatabaseValidator(_reservationRepository, _customerRepository, _apartmentRepository, _hotelRepository);
            await reservationValidator.CheckReservationPutModel(model, reservation);

            reservation.ApartmentId = model.ApartmentId;
            reservation.ReleaseDate = model.ReleaseDate;

            var newReservedApartment = await _apartmentRepository.SingleOrDefaultAsync(x => x.Id == model.ApartmentId);

            newReservedApartment.ReservationId = reservation.Id;
            newReservedApartment.CustomerId = reservation.Customer.Id;

            var oldReservedApartment = await _apartmentRepository.SingleOrDefaultAsync(x => x.Id == reservation.ApartmentId);

            var foreignKeyNullValue = 0;
            oldReservedApartment.ReservationId = foreignKeyNullValue;
            oldReservedApartment.CustomerId = foreignKeyNullValue;

            var responseReservation = await _reservationRepository.UpdateAsync(reservation);
            var responseNewApartment = await _apartmentRepository.UpdateAsync(newReservedApartment);
            var responseOldApartment = await _apartmentRepository.UpdateAsync(oldReservedApartment);

            return responseReservation && responseNewApartment && responseOldApartment;
        }

        public async Task<ReservationDto> Delete(int id)
        {
            var reservation = await _reservationRepository.GetReservationById(id);

            var reservationValidator = new ReservationDatabaseValidator(_reservationRepository, _customerRepository, _apartmentRepository, _hotelRepository);
            await reservationValidator.CheckReservationDeleteModel(reservation);

            var reservedApartment = await _apartmentRepository.SingleOrDefaultAsync(x => x.Id == reservation.ApartmentId);

            var foreignKeyNullValue = 0;
            reservedApartment.ReservationId = foreignKeyNullValue;
            reservedApartment.CustomerId = foreignKeyNullValue;


            var deletingReservationResponse = await _reservationRepository.DeleteAsync(reservation);
            var modifingApartmentResponse = await _apartmentRepository.UpdateAsync(reservedApartment);

            return deletingReservationResponse && modifingApartmentResponse ? _mapper.Map<ReservationDto>(reservation) : null;
        }
    }
}
