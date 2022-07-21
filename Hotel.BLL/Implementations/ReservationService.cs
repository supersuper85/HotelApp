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
        private readonly IRepository<Reservation> _defaultReservationRepository;
        private readonly IRepository<Customer> _defaultCustomerRepository;
        private readonly IRepository<Apartment> _defaultApartmentRepository;
        private readonly IRepository<Hotel> _defaultHotelRepository;
        private readonly IReservationRepository<Reservation> _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationService(IRepository<Reservation> defaultReservationRepository, 
         IReservationRepository<Reservation> reservationRepository, 
         IRepository<Customer> defaultCustomerRepository,
         IRepository<Apartment> defaultApartmentRepository,
         IRepository<Hotel> defaultHotelRepository
        , IMapper mapper)
        {
            _defaultReservationRepository = defaultReservationRepository;
            _defaultCustomerRepository = defaultCustomerRepository;
            _defaultApartmentRepository = defaultApartmentRepository;
            _defaultHotelRepository = defaultHotelRepository;
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }
        public async Task<IList<ReservationDto>> GetAll()
        {
            var reservations = await _defaultReservationRepository.GetAllAsync();
            return _mapper.Map<IList<ReservationDto>>(reservations);
        }

        public async Task<IList<ReservationDto>> GetAllReservationsWithTheirCustomers()
        {
            var reservations = await _reservationRepository.GetAllReservationsWithTheirCustomers();
            return _mapper.Map<IList<ReservationDto>>(reservations);
        }


        public async Task<ReservationDto> GetAReservationWithHisCustomer(int id)
        {
            var reservation = await _reservationRepository.GetAReservationWithHisCustomer(id);
            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<ReservationDto> Add(ReservationDto model)
        {
            var reservationValidator = new ReservationDatabaseValidator(_defaultReservationRepository, _defaultApartmentRepository, _defaultHotelRepository);
            await reservationValidator.CheckReservationPostModel(model);

            var mappedReservation = _mapper.Map<ReservationDto, Reservation>(model);
            var addedReservation = await _defaultReservationRepository.AddAsync(mappedReservation);

            return _mapper.Map<ReservationDto>(addedReservation);

        }
        public async Task<bool> EditAReservation(ReservationDto model)
        {
            var reservation = await _reservationRepository.GetAReservationWithHisCustomer(model.Id);

            var reservationValidator = new ReservationDatabaseValidator(_defaultReservationRepository, _defaultApartmentRepository);
            await reservationValidator.CheckReservationPutModel(model, reservation);

            reservation.Customer.ApartmentId = model.ApartmentId;
            reservation.ApartmentId = model.ApartmentId;
            reservation.ReleaseDate = model.ReleaseDate;

            var reservedApartment = await _defaultApartmentRepository.SingleOrDefaultAsync(x => x.Id == model.ApartmentId);

            reservedApartment.ReservationId = reservation.Id;
            reservedApartment.CustomerId = reservation.Customer.Id;

            var responseReservation = await _defaultReservationRepository.UpdateAsync(reservation);
            var responseCustomer = await _defaultCustomerRepository.UpdateAsync(reservation.Customer);
            var responseApartment = await _defaultApartmentRepository.UpdateAsync(reservedApartment);

            return responseReservation && responseCustomer && responseApartment;
        }

        public async Task<ReservationDto> Delete(int id)
        {
            var reservation = await _reservationRepository.GetAReservationWithHisCustomer(id);

            var reservationValidator = new ReservationDatabaseValidator(_defaultReservationRepository);
            await reservationValidator.CheckReservationDeleteModel(reservation);

            var reservedApartment = await _defaultApartmentRepository.SingleOrDefaultAsync(x => x.Id == reservation.ApartmentId);

            var foreignKeyNullValue = 0;
            reservedApartment.ReservationId = foreignKeyNullValue;
            reservedApartment.CustomerId = foreignKeyNullValue;

            var deletingCustomerResponse = await _defaultCustomerRepository.DeleteAsync(reservation.Customer);
            var deletingReservationResponse = await _defaultReservationRepository.DeleteAsync(reservation);
            var modifingApartmentResponse = await _defaultApartmentRepository.UpdateAsync(reservedApartment);

            return deletingReservationResponse && deletingCustomerResponse && modifingApartmentResponse ? _mapper.Map<ReservationDto>(reservation) : null;
        }

        
    }
}
