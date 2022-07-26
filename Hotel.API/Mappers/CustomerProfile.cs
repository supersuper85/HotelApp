using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.API.Models.CustomerModels;
using HotelApp.API.Models.ReservationModels;

namespace HotelApp.API.Mappers
{
    internal class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerModel, CustomerDto>().ReverseMap();
            CreateMap<CustomerCreateModel, CustomerDto>().ReverseMap();
            CreateMap<CustomerEditModel, CustomerDto>().ReverseMap();
            CreateMap<CustomerDeleteModel, CustomerDto>().ReverseMap();
            CreateMap<ReservationCustomerCreateModel, CustomerDto>().ReverseMap();
        }
    }
}
