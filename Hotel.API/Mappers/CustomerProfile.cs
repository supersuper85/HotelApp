using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.API.Models;

namespace HotelApp.API.Mappers
{
    internal class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerModel, CustomerDto>().ReverseMap();
        }
    }
}
