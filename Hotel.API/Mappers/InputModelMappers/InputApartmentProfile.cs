using AutoMapper;
using HotelApp.API.InputModels;
using HotelApp.BLL.Dto;

namespace HotelApp.API.Mappers.InputModelMappers
{
    public class InputApartmentProfile : Profile
    {
        public InputApartmentProfile()
        {
            CreateMap<ApartmentInputModel, ApartmentDto>().ReverseMap();
        }
    }
}
