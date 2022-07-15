using AutoMapper;
using HotelApp.API.InputModels;
using HotelApp.BLL.Dto;

namespace HotelApp.API.Mappers.InputModelMappers
{
    public class InputCustomerProfile : Profile
    {
        public InputCustomerProfile()
        {
            CreateMap<CustomerInputModel, CustomerDto>().ReverseMap();
        }
    }
}
