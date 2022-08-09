using AuditApp.BLL.Dto;

namespace AuditApp.BLL.Interfaces
{
    public interface IAuditService
    {
        Task<IList<AuditDto>> GetAll();
        Task<AuditDto> Get(int id);
        Task<AuditDto> Add(AuditDto model);
        Task<bool> Edit(AuditDto model);
        Task<AuditDto> Delete(int id);
    }
}
