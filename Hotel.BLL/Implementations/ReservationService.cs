using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Interfaces;
using HotelApp.Data.Entities;
using HotelApp.Data.Interfaces;

namespace HotelApp.BLL.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _defaultRepository;
        private readonly IReservationRepository<Reservation> _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationService(IRepository<Reservation> repository, IReservationRepository<Reservation> reservationRepository,
            IMapper mapper)
        {
            _defaultRepository = repository;
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }
        public async Task<IList<ReservationDto>> GetAll()
        {
            var reservations = await _defaultRepository.GetAllAsync();
            return _mapper.Map<IList<ReservationDto>>(reservations);
        }

        public async Task<IList<ReservationDto>> GetAllReservationsWithTheirCustomers()
        {
            var reservations = await _reservationRepository.GetAllReservationsWithTheirCustomers();
            return _mapper.Map<IList<ReservationDto>>(reservations);
        }

        public async Task<ReservationDto> Get(int id)
        {
            var reservations = await _defaultRepository.GetAllAsync();
            var reservationById = reservations.SingleOrDefault(e => e.Id == id);
            var mappedReservation = _mapper.Map<ReservationDto>(reservationById);

            return mappedReservation;
        }

        public async Task<ReservationDto> Add(ReservationDto model)
        {
            var mappedReservation = _mapper.Map<ReservationDto, Reservation>(model);
            var addedReservation = await _defaultRepository.AddAsync(mappedReservation);

            return _mapper.Map<ReservationDto>(addedReservation);

        }
        public async Task<bool> Edit(ReservationDto model)
        {
            if (await _defaultRepository.ExistsAsync(x => x.Id == model.Id))
            {
                var mappedReservation = _mapper.Map<ReservationDto, Reservation>(model);
                var response = await _defaultRepository.UpdateAsync(mappedReservation);

                return response;
            }
            return false;
        }

        public async Task<ReservationDto> Delete(int id)
        {
            if (await _defaultRepository.ExistsAsync(x => x.Id == id))
            {
                var reservation = await _defaultRepository.SingleOrDefaultAsync(x => x.Id == id);

                var response = await _defaultRepository.DeleteAsync(reservation);
                return response ? _mapper.Map<ReservationDto>(reservation) : null;

            }
            return null;
        }

    }
}
