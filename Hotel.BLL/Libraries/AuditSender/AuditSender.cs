using HotelApp.BLL.Constants;
using HotelApp.BLL.Exceptions;
using HotelApp.BLL.Extensions.Audit.AuditHelpers;
using HotelApp.BLL.Models.AuditModels;
using System.Net.Http.Json;

namespace HotelApp.BLL.Extensions.Audit
{
    public class AuditSender<T> where T : class
    {
        private AuditCreateModel _audit;
        private HttpClient _httpClient;

        public AuditSender(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuditGetModel> ReportPostRequest<T>(T newValuesObj) where T : class
        {
            var auditValidator = new AuditSenderValidator<T>();
            auditValidator.VerifyObjectNotNull(newValuesObj, nameof(newValuesObj));
            auditValidator.VerifyObjectHaveAnId();
            
            try
            {
                _audit = new AuditCreateModel();

                _audit.EntityName = typeof(T).Name;
                _audit.TimeStamp = DateTime.UtcNow;
                _audit.ActionType = "UPDATE";
                _audit.EntityId = GetIdValueFromObject(newValuesObj);
                _audit.OldValues = null;
                _audit.NewValues = Newtonsoft.Json.JsonConvert.SerializeObject(newValuesObj);

                var response = await _httpClient.PostAsJsonAsync(EndPointsPathConstants.RoutePostAudit, _audit);

                var result = await response.Content.ReadFromJsonAsync<AuditGetModel>();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<AuditGetModel> ReportPutRequest<T>(T oldValuesObj, T newValuesObj) where T : class
        {
            var listOfValues = new List<T>();
            listOfValues.Add(oldValuesObj);
            listOfValues.Add(newValuesObj);

            var auditValidator = new AuditSenderValidator<T>();
            auditValidator.VerifyObjectNotNull(oldValuesObj, nameof(oldValuesObj));
            auditValidator.VerifyObjectNotNull(newValuesObj, nameof(newValuesObj));
            auditValidator.VerifyObjectHaveAnId();
            auditValidator.VerifyAuditHaveDifferentValues(oldValuesObj, newValuesObj);

            try
            {
                _audit = new AuditCreateModel();

                _audit.EntityName = typeof(T).Name;
                _audit.TimeStamp = DateTime.UtcNow;
                _audit.ActionType = "UPDATE";
                _audit.EntityId = GetIdValueFromObject(newValuesObj);
                _audit.OldValues = Newtonsoft.Json.JsonConvert.SerializeObject(oldValuesObj);
                _audit.NewValues = Newtonsoft.Json.JsonConvert.SerializeObject(newValuesObj);

                var response = await _httpClient.PostAsJsonAsync(EndPointsPathConstants.RoutePostAudit, _audit);

                var result = await response.Content.ReadFromJsonAsync<AuditGetModel>();
                return result;
            }
            catch
            {
                return null;
            }
            
        }

        public async Task<AuditGetModel> ReportDeleteRequest<T>(T oldValuesObj) where T : class
        { 
            var auditValidator = new AuditSenderValidator<T>();
            auditValidator.VerifyObjectNotNull(oldValuesObj, nameof(oldValuesObj));
            auditValidator.VerifyObjectHaveAnId();

            try
            {
                _audit = new AuditCreateModel();

                _audit.EntityName = typeof(T).Name;
                _audit.TimeStamp = DateTime.UtcNow;
                _audit.ActionType = "UPDATE";
                _audit.EntityId = GetIdValueFromObject(oldValuesObj);
                _audit.OldValues = Newtonsoft.Json.JsonConvert.SerializeObject(oldValuesObj);
                _audit.NewValues = null;

                var response = await _httpClient.PostAsJsonAsync(EndPointsPathConstants.RoutePostAudit, _audit);

                var result = await response.Content.ReadFromJsonAsync<AuditGetModel>();
                return result;
            }
            catch
            {
                return null;
            }

        }

        private int GetIdValueFromObject<T>(T obj)
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
