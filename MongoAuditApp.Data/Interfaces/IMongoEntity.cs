using MongoDB.Bson;

namespace MongoAuditApp.Data.Interfaces
{
    public interface IMongoEntity
    {
        ObjectId Id { get; set; }
    }
}
