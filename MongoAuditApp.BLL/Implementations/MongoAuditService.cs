using MongoAuditApp.BLL.Dto;
using MongoAuditApp.BLL.Interfaces;
using AutoMapper;
using MongoAuditApp.Data.Entities;
using MongoAuditApp.Data.Interfaces;
using MongoDB.Bson;

namespace MongoAuditApp.BLL.Implementations
{
    public class MongoAuditService : IMongoAuditService
    {
        private readonly IMongoAuditRepository _auditRepository;
        private readonly IMapper _mapper;

        public MongoAuditService(IMongoAuditRepository auditRepository,
            IMapper mapper)
        {
            _auditRepository = auditRepository;
            _mapper = mapper;
        }

        public async Task<MongoAuditDto> Get(string id)
        {
            var audit = await _auditRepository.GetAsync(ObjectId.Parse(id));
            var mappedAudit = _mapper.Map<MongoAuditDto>(audit);

            return mappedAudit;
        }

        public async Task<MongoAuditDto> Add(MongoAuditDto model)
        {
            var mappedAudit = _mapper.Map<MongoAuditDto, MongoAudit>(model);
            var addedAudit = await _auditRepository.AddAsync(mappedAudit);

            return _mapper.Map<MongoAuditDto>(addedAudit);
        }

    }
}
