using MongoAuditApp.Data.Entities;
using MongoAuditApp.Data.Implementations;
using MongoAuditApp.Data.Interfaces;
using MongoDB.Driver;

namespace MongoAuditApp.Data.Implementations
{
    public class MongoAuditRepository : BaseMongoRepository<MongoAudit>, IMongoAuditRepository
    {
        public MongoAuditRepository(IMongoDatabase database)
            : base(database)
        {

        }
    }
}
