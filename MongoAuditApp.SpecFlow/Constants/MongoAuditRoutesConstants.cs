namespace MongoAuditApp.SpecFlow.Constants
{
    public static class MongoAuditRoutesConstants
    {
        public const string PostPath = "https://localhost:7091/api/customer/create";
        public const string GetPath = "https://localhost:7091/api/customer/getbyid";
        public const string EditPath = "https://localhost:7091/api/customer/edit";
        public const string DeletePath = "https://localhost:7091/api/customer/delete";

        public const string CreatedMongoAuditKey = "RecentlyAddedAudit";
    }
}
