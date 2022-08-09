using HotelApp.BLL.Models.AuditModels;

namespace HotelApp.BLL.Extensions.Audit.AuditHelpers
{
    public class AuditSenderPropertiesSetter
    {
        public void SetObjectInitialized(ref bool objectInitialized, bool status)
        {
            objectInitialized = status;
        }
        public void SetObjectHaveNewValues(ref bool objectHaveNewValues, bool status)
        {
            objectHaveNewValues = status;
        }
        public void SetAuditType<T>(ref string auditType) where T : class
        {
            auditType = typeof(T).Name;
        }
        public void SetAuditTypeNull(ref string auditType)
        {
            auditType = null;
        }
        public void SetHttpClient(ref HttpClient httpClient, HttpClient injectedHttpClient)
        {
            httpClient = injectedHttpClient;
        }
        public void SetStringToUpperCase(ref string actionType)
        {
            actionType = actionType.ToUpper();
        }
        public void SetActionType(ref AuditCreateModel audit, string actionType)
        {
            audit.ActionType = actionType;
        }
        public void SetAuditInitialValues<T>(ref AuditCreateModel audit) where T : class
        {
            audit = new AuditCreateModel();

            audit.EntityName = typeof(T).Name;
            audit.TimeStamp = DateTime.UtcNow;
        }
        public void SetOldValues<T>(ref AuditCreateModel audit, T obj) where T : class
        {
            switch (audit.ActionType)
            {
                case "INSERT":
                    audit.OldValues = null;
                    break;
                case "DELETE":
                    audit.EntityId = GetIdValueFromObject(obj);
                    audit.OldValues = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                    break;
                case "UPDATE":
                    audit.EntityId = GetIdValueFromObject(obj);
                    audit.OldValues = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                    break;
                default:
                    break;
            }
        }
        public void SetNewValues<T>(ref AuditCreateModel audit, T obj) where T : class
        {
            switch (audit.ActionType)
            {
                case "INSERT":
                    audit.EntityId = GetIdValueFromObject(obj);
                    audit.NewValues = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                    break;
                case "DELETE":
                    audit.NewValues = null;
                    break;
                case "UPDATE":
                    audit.NewValues = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                    break;
                default:
                    break;
            }
        }
        private int GetIdValueFromObject<T>(T obj) where T : class
        {
            Type typeT = typeof(T);
            foreach (System.Reflection.PropertyInfo pi in typeT.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if (pi.Name == "Id")
                {
                    object propertyValue = typeT.GetProperty(pi.Name).GetValue(obj, null);
                    return (int)propertyValue;
                }
            }
            return 0;
        }
    }
}
