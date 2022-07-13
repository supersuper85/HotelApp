using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Interfaces;
using HotelApp.Data.Entities;
using HotelApp.Data.Interfaces;

namespace HotelApp.BLL.Implementations
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository<Apartment> _hotelRoomRepository;
        private readonly IRepository<Apartment> _defaultRepository;
        private readonly IMapper _mapper;

        public ApartmentService(IApartmentRepository<Apartment> hotelRoomRepository, IRepository<Apartment> defaultRepository,
            IMapper mapper)
        {
            _hotelRoomRepository = hotelRoomRepository;
            _defaultRepository = defaultRepository;
            _mapper = mapper;
        }
        public async Task<IList<ApartmentDto>> GetAll()
        {
            var entities = await _defaultRepository.GetAllAsync();
            return _mapper.Map<IList<ApartmentDto>>(entities);
        }

        public async Task<ApartmentDto> Get(int id)
        {
            var entities = await _defaultRepository.GetAllAsync();
            var entityById = entities.SingleOrDefault(e => e.Id == id);
            var mapped = _mapper.Map<ApartmentDto>(entityById);

            return mapped;
        }

        public async Task<ApartmentDto> Add(ApartmentDto model)
        {
            //verify if hotelroom exists in database!!!
            var mappedBook = _mapper.Map<ApartmentDto, Apartment>(model);
            var addedBook = await _defaultRepository.AddAsync(mappedBook);

            return _mapper.Map<ApartmentDto>(addedBook);

        }
        public async Task<bool> Edit(ApartmentDto model)
        {
            if (await _defaultRepository.ExistsAsync(x => x.Id == model.Id))
            {
                var mappedBook = _mapper.Map<ApartmentDto, Apartment>(model);
                var response = await _defaultRepository.UpdateAsync(mappedBook);

                return response;
            }
            return false;
        }

        public async Task<ApartmentDto> Delete(int id)
        {
            if (await _defaultRepository.ExistsAsync(x => x.Id == id))
            {
                var book = await _defaultRepository.SingleOrDefaultAsync(x => x.Id == id);

                var response = await _defaultRepository.DeleteAsync(book);
                return response ? _mapper.Map<ApartmentDto>(book) : null;

            }
            return null;
        }

        public async Task<IList<ApartmentDto>> GetAllAvailableHotelRooms()
        {
            var books = await _hotelRoomRepository.GetAllAvailableHotelRooms();
            return _mapper.Map<IList<ApartmentDto>>(books);

        }
    }
}
