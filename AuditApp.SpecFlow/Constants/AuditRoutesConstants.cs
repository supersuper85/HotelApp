namespace AuditApp.SpecFlow.Constants
{
    public static class AuditRoutesConstants
    {
        public const string PostPath = "https://localhost:7261/api/audit/create";
        public const string GetPath = "https://localhost:7261/api/audit/get";
        public const string DeletePath = "https://localhost:7261/api/audit/delete";

        public const string CreatedAuditKey = "RecentlyAddedAudit";
    }
}
