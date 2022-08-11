using AutoMapper;
using MongoAuditApp.API.Models;
using MongoAuditApp.BLL.Dto;

namespace MongoAuditApp.API.Mappers
{
    public class MongoAuditProfile : Profile
    {
        public MongoAuditProfile()
        {
            CreateMap<MongoAuditModel, MongoAuditDto>().ReverseMap();
            CreateMap<MongoAuditCreateModel, MongoAuditDto>().ReverseMap();
        }
    }
}
