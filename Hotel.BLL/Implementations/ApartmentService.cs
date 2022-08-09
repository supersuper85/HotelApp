using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Interfaces;
using HotelApp.BLL.Validations;
using HotelApp.Data.Entities;
using HotelApp.Data.Interfaces;

namespace HotelApp.BLL.Implementations
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IMapper _mapper;

        public ApartmentService(IApartmentRepository apartmentRepository,
            IMapper mapper)
        {
            _apartmentRepository = apartmentRepository;
            _mapper = mapper;
        }
        public async Task<IList<ApartmentDto>> GetAll()
        {
            var apartments = await _apartmentRepository.GetAllAsync();
            return _mapper.Map<IList<ApartmentDto>>(apartments);
        }

        public async Task<ApartmentDto> Get(int id)
        {
            var apartment = await _apartmentRepository.GetApartmentById(id);
            var mapped = _mapper.Map<ApartmentDto>(apartment);

            return mapped;
        }

        public async Task<ApartmentDto> Add(ApartmentDto model)
        {
            var apartmentValidator = new ApartmentDatabaseValidator(null, null, _apartmentRepository, null);
            await apartmentValidator.CheckApartmentPostModel(model);

            var mappedApartment = _mapper.Map<ApartmentDto, Apartment>(model);
            var addedApartment = await _apartmentRepository.AddAsync(mappedApartment);

            return _mapper.Map<ApartmentDto>(addedApartment);

        }
        public async Task<bool> Edit(ApartmentDto model)
        {
            var entity = await _apartmentRepository.SingleOrDefaultAsync(x => x.Id == model.Id);

            var apartmentValidator = new ApartmentDatabaseValidator(null, null, _apartmentRepository, null);
            await apartmentValidator.CheckApartmentPutModel(entity, model);

            entity.NumberOfRooms = model.NumberOfRooms;
            entity.ApartmentNumber = model.ApartmentNumber;
            entity.DailyRentInEuro = model.DailyRentInEuro;

            var response = await _apartmentRepository.UpdateAsync(entity);

            return response;
        }

        public async Task<ApartmentDto> Delete(int id)
        {
            var apartment = await _apartmentRepository.SingleOrDefaultAsync(x => x.Id == id);

            var apartmentValidator = new ApartmentDatabaseValidator(null, null, _apartmentRepository, null);
            await apartmentValidator.CheckApartmentDeleteModel(apartment);
                
            var response = await _apartmentRepository.DeleteAsync(apartment);
            return response ? _mapper.Map<ApartmentDto>(apartment) : null;

        }

        public async Task<IList<ApartmentDto>> GetAllAvailableHotelRooms()
        {
            var apartments = await _apartmentRepository.GetAllAvailableApartments();
            return _mapper.Map<IList<ApartmentDto>>(apartments);

        }
    }
}
