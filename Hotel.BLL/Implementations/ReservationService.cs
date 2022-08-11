using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Extensions.Audit;
using HotelApp.BLL.Interfaces;
using HotelApp.BLL.Libraries.ObjectCloner;
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
        private readonly HttpClient _httpClient;
        public ReservationService(
         IReservationRepository reservationRepository,
         ICustomerRepository customerRepository,
         IApartmentRepository apartmentRepository,
         IRepository<Hotel> defaultHotelRepository,
         IMapper mapper,
         HttpClient httpClient)
        {
            _customerRepository = customerRepository;
            _apartmentRepository = apartmentRepository;
            _hotelRepository = defaultHotelRepository;
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _httpClient = httpClient;
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

            var reservationAuditSender = new AuditSender<Reservation>(_httpClient);

            var addedReservation = await _reservationRepository.AddAsync(mappedReservation);

            var addingReservationResponse = addedReservation != null;
            if (addingReservationResponse)
                reservationAuditSender.ReportPostRequest(addedReservation);

            var newValueOf_Apartment = await _apartmentRepository.SingleOrDefaultAsync(x => x.Id == model.ApartmentId);

            var apartmentObjectCloner = new ObjectCloner<Apartment>();
            var oldValuesOf_ReservedApartment = apartmentObjectCloner.Clone(newValueOf_Apartment);

            newValueOf_Apartment.ReservationId = addedReservation.Id;
            newValueOf_Apartment.CustomerId = customerId;


            var apartmentAuditSender = new AuditSender<Apartment>(_httpClient);

            var modifingApartmentResponse = await _apartmentRepository.UpdateAsync(newValueOf_Apartment);
            if (modifingApartmentResponse)
                apartmentAuditSender.ReportPutRequest(oldValuesOf_ReservedApartment, newValueOf_Apartment);

            return modifingApartmentResponse ? _mapper.Map<ReservationDto>(addedReservation) : null;


        }
        public async Task<bool> EditAReservation(ReservationDto model)
        {
            var newValueOf_Reservation = await _reservationRepository.GetReservationById(model.Id);
            
            var reservationValidator = new ReservationDatabaseValidator(_reservationRepository, _customerRepository, _apartmentRepository, _hotelRepository);
            await reservationValidator.CheckReservationPutModel(model, newValueOf_Reservation);


            var reservationAuditSender = new AuditSender<Reservation>(_httpClient);
            var apartmentAuditSender = new AuditSender<Apartment>(_httpClient);

            var reservationObjectCloner = new ObjectCloner<Reservation>();
            var apartmentObjectCloner = new ObjectCloner<Apartment>();

            var oldValuesOf_Reservation = reservationObjectCloner.Clone(newValueOf_Reservation);

            newValueOf_Reservation.ApartmentId = model.ApartmentId;
            newValueOf_Reservation.ReleaseDate = model.ReleaseDate;


            var newValueOf_NewReservedApartment = await _apartmentRepository.SingleOrDefaultAsync(x => x.Id == model.ApartmentId);

            var oldValuesOf_NewReservedApartment = apartmentObjectCloner.Clone(newValueOf_NewReservedApartment);

            newValueOf_NewReservedApartment.ReservationId = newValueOf_Reservation.Id;
            newValueOf_NewReservedApartment.CustomerId = newValueOf_Reservation.Customer.Id;


            var newValueOf_OldReservedApartment = await _apartmentRepository.SingleOrDefaultAsync(x => x.Id == newValueOf_Reservation.ApartmentId);

            var oldValuesOf_OldReservedApartment = apartmentObjectCloner.Clone(newValueOf_OldReservedApartment);

            var foreignKeyNullValue = 0;
            newValueOf_OldReservedApartment.ReservationId = foreignKeyNullValue;
            newValueOf_OldReservedApartment.CustomerId = foreignKeyNullValue;


            var responseReservation = await _reservationRepository.UpdateAsync(newValueOf_Reservation);
            if (responseReservation)
                apartmentAuditSender.ReportPutRequest(oldValuesOf_Reservation, newValueOf_Reservation);

            var responseNewApartment = await _apartmentRepository.UpdateAsync(newValueOf_NewReservedApartment);
            if (responseNewApartment)
                apartmentAuditSender.ReportPutRequest(oldValuesOf_NewReservedApartment, newValueOf_NewReservedApartment);

            var responseOldApartment = await _apartmentRepository.UpdateAsync(newValueOf_OldReservedApartment);
            if (responseOldApartment)
                apartmentAuditSender.ReportPutRequest(oldValuesOf_OldReservedApartment, newValueOf_OldReservedApartment);


            return responseReservation && responseNewApartment && responseOldApartment;
        }

        public async Task<ReservationDto> Delete(int id)
        {
            var oldValueOf_Reservation = await _reservationRepository.GetReservationById(id);

            var reservationValidator = new ReservationDatabaseValidator(_reservationRepository, _customerRepository, _apartmentRepository, _hotelRepository);
            await reservationValidator.CheckReservationDeleteModel(oldValueOf_Reservation);

            var newValueOf_ReservedApartment = await _apartmentRepository.SingleOrDefaultAsync(x => x.Id == oldValueOf_Reservation.ApartmentId);

            var reservationAuditSender = new AuditSender<Reservation>(_httpClient);
            var apartmentAuditSender = new AuditSender<Apartment>(_httpClient);

            var apartmentObjectCloner = new ObjectCloner<Apartment>();
            var oldValuesOf_ReservedApartment = apartmentObjectCloner.Clone(newValueOf_ReservedApartment);

            var foreignKeyNullValue = 0;
            newValueOf_ReservedApartment.ReservationId = foreignKeyNullValue;
            newValueOf_ReservedApartment.CustomerId = foreignKeyNullValue;


            var deletingReservationResponse = await _reservationRepository.DeleteAsync(oldValueOf_Reservation);
            if (deletingReservationResponse)
                reservationAuditSender.ReportDeleteRequest(oldValueOf_Reservation);

            var modifingApartmentResponse = await _apartmentRepository.UpdateAsync(newValueOf_ReservedApartment);
            if (deletingReservationResponse)
                apartmentAuditSender.ReportPutRequest(oldValuesOf_ReservedApartment, newValueOf_ReservedApartment);

            return deletingReservationResponse && modifingApartmentResponse ? _mapper.Map<ReservationDto>(oldValueOf_Reservation) : null;
        }
    }
}
