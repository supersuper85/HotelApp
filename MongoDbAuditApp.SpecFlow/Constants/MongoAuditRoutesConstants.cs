namespace MongoDbAuditApp.SpecFlow.Constants
{
    public static class MongoAuditRoutesConstants
    {
        public const string PostPath = "https://localhost:7170/api/mongoaudit/create";
        public const string GetPath = "https://localhost:7170/api/mongoaudit/get";
        public const string DeletePath = "https://localhost:7170/api/mongoaudit/delete";

        public const string CreatedMongoAuditKey = "RecentlyAddedAudit";
    }
}
