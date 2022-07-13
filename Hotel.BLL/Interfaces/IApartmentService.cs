using HotelApp.BLL.Dto;

namespace HotelApp.BLL.Interfaces
{
    public interface IApartmentService
    {
        Task<IList<ApartmentDto>> GetAll();
        Task<ApartmentDto> Get(int id);
        Task<IList<ApartmentDto>> GetAllAvailableHotelRooms();
        Task<ApartmentDto> Add(ApartmentDto model);
        Task<bool> Edit(ApartmentDto model);
        Task<ApartmentDto> Delete(int id);
    }
}
