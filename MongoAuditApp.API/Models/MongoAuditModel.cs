using MongoDB.Bson;

namespace MongoAuditApp.API.Models
{
    public class MongoAuditModel
    {
        public string Id { get; set; }
        public int EntityId { get; set; }
        public string EntityName { get; set; }
        public string ActionType { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
    }
}
