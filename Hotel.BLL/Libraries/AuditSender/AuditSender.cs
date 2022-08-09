using HotelApp.BLL.Constants;
using HotelApp.BLL.Exceptions;
using HotelApp.BLL.Extensions.Audit.AuditHelpers;
using HotelApp.BLL.Models.AuditModels;
using System.Net.Http.Json;

namespace HotelApp.BLL.Extensions.Audit
{
    public class AuditSender
    {
        private AuditCreateModel _audit;
        private HttpClient _httpClient;

        private bool _objectInitialized;
        private bool _objectHaveNewValues;

        private string? _auditType;

        #region SetInitialValuesRegion
        public void SetInitialValues<T>(HttpClient httpClient, string actionType, T obj) where T : class
        {
            var auditPropertiesSetter = new AuditSenderPropertiesSetter();
            auditPropertiesSetter.SetStringToUpperCase(ref actionType);

            var auditValidator = new AuditSenderValidator();
            auditValidator.VerifyObjectIsNotInitialized(_objectInitialized);
            auditValidator.VerifyActionType(actionType);
            auditValidator.VerifyObjectHaveIdOnNonInsert<T>(actionType);

            auditPropertiesSetter.SetAuditInitialValues<T>(ref _audit);
            auditPropertiesSetter.SetActionType(ref _audit, actionType);
            auditPropertiesSetter.SetOldValues(ref _audit, obj);

            auditPropertiesSetter.SetHttpClient(ref _httpClient, httpClient);
            auditPropertiesSetter.SetAuditType<T>(ref _auditType);
            auditPropertiesSetter.SetObjectInitialized(ref _objectInitialized, true);
        }
        #endregion


        #region SetNewValuesRegion
        public void SetNewValues<T>(T obj) where T : class
        {
            var auditValidator = new AuditSenderValidator();
            auditValidator.VerifyObjectDoesntHaveNewValues(_objectHaveNewValues);
            auditValidator.VerifyObjectHaveIdOnNonDelete<T>(_audit.ActionType);
            auditValidator.VerifyNewValueObjectIsSameTypeAsOldValueObject<T>(_auditType);

            var auditPropertiesSetter = new AuditSenderPropertiesSetter();
            auditPropertiesSetter.SetNewValues(ref _audit, obj);
            auditPropertiesSetter.SetObjectHaveNewValues(ref _objectHaveNewValues, true);
        }
        #endregion


        #region SendPostRequestRegion
        public async Task<AuditGetModel> SendPostRequest()
        {
            ValidateSendPostRequest();
            var result = await PostRequestToAuditApi();
            ResetConfiguration();

            return result;
        }
        private void ValidateSendPostRequest()
        {
            var auditValidator = new AuditSenderValidator();
            auditValidator.VerifyObjectIsInitialized(_objectInitialized);
            auditValidator.VerifyObjectHaveSetNewValues(_objectHaveNewValues);
        }
        private async Task<AuditGetModel> PostRequestToAuditApi()
        {
            var response = await _httpClient.PostAsJsonAsync(EndPointsPathConstants.RoutePostAudit, _audit);

            var result = await response.Content.ReadFromJsonAsync<AuditGetModel>();
            return result;
        }
        #endregion


        #region ResetConfigurationRegion
        public void ResetConfiguration()
        {
            var auditPropertiesSetter = new AuditSenderPropertiesSetter();
            auditPropertiesSetter.SetObjectInitialized(ref _objectInitialized, false);
            auditPropertiesSetter.SetObjectHaveNewValues(ref _objectHaveNewValues, false);
            auditPropertiesSetter.SetAuditTypeNull(ref _auditType);
        }
        #endregion
    }
}
