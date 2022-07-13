using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.Data.Entities;

namespace HotelApp.BLL.Mappers
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();
        }
    }
}
