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
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        public ApartmentService(IApartmentRepository apartmentRepository,
            IMapper mapper,
            HttpClient httpClient)
        {
            _apartmentRepository = apartmentRepository;
            _mapper = mapper;
            _httpClient = httpClient;
        }
        public async Task<IList<ApartmentDto>> GetAll()
        {
            var apartments = await _apartmentRepository.GetAllAsync();
            return _mapper.Map<IList<ApartmentDto>>(apartments);
        }
        public async Task<IList<ApartmentDto>> GetAllAvailableHotelRooms()
        {
            var apartments = await _apartmentRepository.GetAllAvailableApartments();
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

            var newApartment = _mapper.Map<ApartmentDto, Apartment>(model);

            var auditSender = new AuditSender<Apartment>(_httpClient);

            var addedApartment = await _apartmentRepository.AddAsync(newApartment);

            var operationIsSuccesfully = addedApartment != null;
            if (operationIsSuccesfully)
                auditSender.ReportPostRequest(addedApartment);

            return _mapper.Map<ApartmentDto>(addedApartment);

        }
        public async Task<bool> Edit(ApartmentDto model)
        {
            var newValueOf_Apartment = await _apartmentRepository.SingleOrDefaultAsync(x => x.Id == model.Id);

            var apartmentValidator = new ApartmentDatabaseValidator(null, null, _apartmentRepository, null);
            await apartmentValidator.CheckApartmentPutModel(newValueOf_Apartment, model);

            var auditSender = new AuditSender<Apartment>(_httpClient);
            var objectCloner = new ObjectCloner<Apartment>();
            var oldValuesOf_Apartment = objectCloner.Clone(newValueOf_Apartment);

            newValueOf_Apartment.NumberOfRooms = model.NumberOfRooms;
            newValueOf_Apartment.ApartmentNumber = model.ApartmentNumber;
            newValueOf_Apartment.DailyRentInEuro = model.DailyRentInEuro;

            var operationIsSuccesfully = await _apartmentRepository.UpdateAsync(newValueOf_Apartment);
            if (operationIsSuccesfully)
                auditSender.ReportPutRequest(oldValuesOf_Apartment, newValueOf_Apartment);

            return operationIsSuccesfully;
        }

        public async Task<ApartmentDto> Delete(int id)
        {
            var oldValuesOf_Apartment = await _apartmentRepository.SingleOrDefaultAsync(x => x.Id == id);

            var apartmentValidator = new ApartmentDatabaseValidator(null, null, _apartmentRepository, null);
            await apartmentValidator.CheckApartmentDeleteModel(oldValuesOf_Apartment);

            var auditSender = new AuditSender<Apartment>(_httpClient);

            var operationIsSuccesfully = await _apartmentRepository.DeleteAsync(oldValuesOf_Apartment);

            if (operationIsSuccesfully)
                auditSender.ReportDeleteRequest(oldValuesOf_Apartment);

            return operationIsSuccesfully ? _mapper.Map<ApartmentDto>(oldValuesOf_Apartment) : null;
        }
    }
}
