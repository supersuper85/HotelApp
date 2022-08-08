using AuditApp.Data.Entities;
using AuditApp.Data.Entities.Context;
using AuditApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuditApp.Data.Implementations
{
    public class AuditRepository : DefaultRepository<Audit>, IAuditRepository
    {
        public AuditRepository(DataBaseContext context) : base(context)
        {

        }

        public async Task<Audit> GetAuditByIdAsNoTracking(int id)
        {
            var result = await _entities.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
