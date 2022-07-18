using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.API.Models.CustomerModels;

namespace HotelApp.API.Mappers.ModelMappers
{
    internal class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerModel, CustomerDto>().ReverseMap();
        }
    }
}
