using AuditApp.BLL.Dto;
using AuditApp.Data.Entities;
using AutoMapper;

namespace AuditApp.BLL.Mappers
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditDto, Audit>().ReverseMap();
        }
    }
}
