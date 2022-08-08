using AuditApp.Data.Entities;

namespace AuditApp.Data.Interfaces
{
    public interface IAuditRepository : IRepository<Audit>
    {
        Task<Audit> GetAuditByIdAsNoTracking(int id);
    }
}
