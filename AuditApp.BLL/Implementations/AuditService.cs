using AuditApp.BLL.Dto;
using AuditApp.BLL.Interfaces;
using AuditApp.BLL.Validations;
using AuditApp.Data.Entities;
using AuditApp.Data.Interfaces;
using AutoMapper;

namespace AuditApp.BLL.Implementations
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepository;
        private readonly IMapper _mapper;

        public AuditService(IAuditRepository auditRepository,
            IMapper mapper)
        {
            _auditRepository = auditRepository;
            _mapper = mapper;
        }
        public async Task<IList<AuditDto>> GetAll()
        {
            var audits = await _auditRepository.GetAllAsync();
            return _mapper.Map<IList<AuditDto>>(audits);
        }

        public async Task<AuditDto> Get(int id)
        {
            var audit = await _auditRepository.SingleOrDefaultAsync(x => x.Id == id);
            var mappedAudit = _mapper.Map<AuditDto>(audit);

            return mappedAudit;
        }

        public async Task<AuditDto> Add(AuditDto model)
        {
            var mappedAudit = _mapper.Map<AuditDto, Audit>(model);
            var addedAudit = await _auditRepository.AddAsync(mappedAudit);

            return _mapper.Map<AuditDto>(addedAudit);
        }
        public async Task<bool> Edit(AuditDto model)
        {
            var entity = await _auditRepository.GetAuditByIdAsNoTracking(model.Id);

            var auditValidator = new AuditDatabaseValidator(_auditRepository);
            auditValidator.CheckAuditPutModel(entity, model);

            var mappedModel = _mapper.Map<Audit>(model);

            var response = await _auditRepository.UpdateAsync(mappedModel);

            return response;
        }

        public async Task<AuditDto> Delete(int id)
        {
            var audit = await _auditRepository.SingleOrDefaultAsync(x => x.Id == id);

            var auditValidator = new AuditDatabaseValidator(_auditRepository);
            auditValidator.CheckAuditDeleteModel(audit);

            var response = await _auditRepository.DeleteAsync(audit);
            return response ? _mapper.Map<AuditDto>(audit) : null;

        }
    }
}
