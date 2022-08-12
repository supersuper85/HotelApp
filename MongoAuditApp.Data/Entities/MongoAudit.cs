using MongoAuditApp.Data.Interfaces;
using MongoDB.Bson;

namespace MongoAuditApp.Data.Entities
{
    public class MongoAudit : IMongoEntity
    {
        public ObjectId Id { get; set; }
        public int EntityId { get; set; }
        public string EntityName { get; set; }
        public string ActionType { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
    }
}
