using MongoAuditApp.BLL.Dto;

namespace MongoAuditApp.BLL.Interfaces
{
    public interface IMongoAuditService
    {
        Task<MongoAuditDto> Get(string id);
        Task<MongoAuditDto> Add(MongoAuditDto model);
        Task<MongoAuditDto> Delete(string id);
    }
}
