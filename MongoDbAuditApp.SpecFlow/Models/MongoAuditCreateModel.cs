namespace MongoDbAuditApp.Specflow.Models
{
    public class MongoAuditCreateModel
    {
        public int EntityId { get; set; }
        public string EntityName { get; set; }
        public string ActionType { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
    }
}
