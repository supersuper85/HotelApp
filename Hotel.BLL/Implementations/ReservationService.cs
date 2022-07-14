using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Interfaces;
using HotelApp.Data.Entities;
using HotelApp.Data.Interfaces;

namespace HotelApp.BLL.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IMapper _mapper;

        public ReservationService(IRepository<Reservation> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IList<ReservationDto>> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IList<ReservationDto>>(entities);
        }

        public async Task<ReservationDto> Get(int id)
        {
            var entities = await _repository.GetAllAsync();
            var entityById = entities.SingleOrDefault(e => e.Id == id);
            var mapped = _mapper.Map<ReservationDto>(entityById);

            return mapped;
        }

        public async Task<ReservationDto> Add(ReservationDto model)
        {
            //verify if borrower exists in database!!!
            var mappedBook = _mapper.Map<ReservationDto, Reservation>(model);
            var addedBook = await _repository.AddAsync(mappedBook);

            return _mapper.Map<ReservationDto>(addedBook);

        }
        public async Task<bool> Edit(ReservationDto model)
        {
            if (await _repository.ExistsAsync(x => x.Id == model.Id))
            {
                var mappedBook = _mapper.Map<ReservationDto, Reservation>(model);
                var response = await _repository.UpdateAsync(mappedBook);

                return response;
            }
            return false;
        }

        public async Task<ReservationDto> Delete(int id)
        {
            if (await _repository.ExistsAsync(x => x.Id == id))
            {
                var book = await _repository.SingleOrDefaultAsync(x => x.Id == id);

                var response = await _repository.DeleteAsync(book);
                return response ? _mapper.Map<ReservationDto>(book) : null;

            }
            return null;
        }

    }
}
