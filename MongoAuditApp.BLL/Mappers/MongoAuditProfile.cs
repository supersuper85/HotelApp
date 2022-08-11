using MongoAuditApp.BLL.Dto;
using AutoMapper;
using MongoAuditApp.Data.Entities;

namespace MongoAuditApp.BLL.Mappers
{
    public class MongoAuditProfile : Profile
    {
        public MongoAuditProfile()
        {
            CreateMap<MongoAuditDto, MongoAudit>().ReverseMap();
        }
    }
}
