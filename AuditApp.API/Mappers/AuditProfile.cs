using AuditApp.API.Models;
using AuditApp.BLL.Dto;
using AutoMapper;

namespace AuditApp.API.Mappers
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditModel, AuditDto>().ReverseMap();
            CreateMap<AuditCreateModel, AuditDto>().ReverseMap();
        }
    }
}
