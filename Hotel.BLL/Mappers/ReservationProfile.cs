using AutoMapper;
using HotelApp.BLL.Dto;
using HotelApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.BLL.Mappers
{
    internal class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<ReservationDto, Reservation>().ReverseMap();
        }
    }
}
