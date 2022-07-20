using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Interfaces;
using HotelApp.Data.Entities;
using HotelApp.Data.Interfaces;

namespace HotelApp.BLL.Implementations
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository<Apartment> _apartmentRepository;
        private readonly IRepository<Apartment> _defaultRepository;
        private readonly IMapper _mapper;

        public ApartmentService(IApartmentRepository<Apartment> hotelRoomRepository, IRepository<Apartment> defaultRepository,
            IMapper mapper)
        {
            _apartmentRepository = hotelRoomRepository;
            _defaultRepository = defaultRepository;
            _mapper = mapper;
        }
        public async Task<IList<ApartmentDto>> GetAll()
        {
            var apartments = await _defaultRepository.GetAllAsync();
            return _mapper.Map<IList<ApartmentDto>>(apartments);
        }

        public async Task<ApartmentDto> Get(int id)
        {
            var apartments = await _defaultRepository.GetAllAsync();
            var apartmentsyById = apartments.SingleOrDefault(e => e.Id == id);
            var mapped = _mapper.Map<ApartmentDto>(apartmentsyById);

            return mapped;
        }

        public async Task<ApartmentDto> Add(ApartmentDto model)
        {
            var mappedApartment = _mapper.Map<ApartmentDto, Apartment>(model);
            var addedApartment = await _defaultRepository.AddAsync(mappedApartment);

            return _mapper.Map<ApartmentDto>(addedApartment);

        }
        public async Task<bool> Edit(ApartmentDto model)
        {
            if (await _defaultRepository.ExistsAsync(x => x.Id == model.Id))
            {
                var mappedApartment = _mapper.Map<ApartmentDto, Apartment>(model);
                var response = await _defaultRepository.UpdateAsync(mappedApartment);

                return response;
            }
            return false;
        }

        public async Task<ApartmentDto> Delete(int id)
        {
            if (await _defaultRepository.ExistsAsync(x => x.Id == id))
            {
                var apartment = await _defaultRepository.SingleOrDefaultAsync(x => x.Id == id);

                var response = await _defaultRepository.DeleteAsync(apartment);
                return response ? _mapper.Map<ApartmentDto>(apartment) : null;

            }
            return null;
        }

        public async Task<IList<ApartmentDto>> GetAllAvailableHotelRooms()
        {
            var apartments = await _apartmentRepository.GetAllAvailableApartments();
            return _mapper.Map<IList<ApartmentDto>>(apartments);

        }
    }
}
