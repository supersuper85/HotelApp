using AutoMapper;
using MongoAuditApp.API.Models;
using MongoAuditApp.BLL.Dto;

namespace MongoAuditApp.API.Mappers
{
    public class MongoAuditProfile : Profile
    {
        public MongoAuditProfile()
        {
            CreateMap<MongoAuditModel, MongoAuditDto>();
            CreateMap<MongoAuditCreateModel, MongoAuditDto>().ReverseMap();

            CreateMap<MongoAuditDto, MongoAuditModel>().AfterMap((src, dest) =>
            {
                dest.Id = src.Id.ToString();
            });

        }
    }
}
