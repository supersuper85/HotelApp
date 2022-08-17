using AuditApp.Specflow.Models;
using AuditApp.SpecFlow.Constants;
using MongoDB.Bson;
using System.Net.Http.Json;
using System.Text;
using TechTalk.SpecFlow;

namespace AuditApp.SpecFlow.Drives
{
    public class AuditDriver
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;

        public AuditCreateModel RequestModel { get; set; }

        public AuditDriver(HttpClient httpClient, ScenarioContext scenarioContext)
        {
            _httpClient = httpClient;
            _scenarioContext = scenarioContext;
        }

        #region Act Methods
        public async Task PostAudit()
        {
            var response = await _httpClient.PostAsJsonAsync(AuditRoutesConstants.PostPath, RequestModel);

            var newAddedAudit = await response.Content.ReadFromJsonAsync<AuditModel>();

            _scenarioContext.Add(AuditRoutesConstants.CreatedAuditKey, newAddedAudit);
        }


        public async Task<bool> DeleteRecentlyAddedAudit()
        {
            var newAddedAudit = _scenarioContext.Get<AuditModel>(AuditRoutesConstants.CreatedAuditKey);
            var auditDeleteModel = new AuditDeleteModel();
            auditDeleteModel.Id = newAddedAudit.Id; 

            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(auditDeleteModel), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(AuditRoutesConstants.DeletePath)
            };

            var response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
        #endregion



        #region Assert Methods
        public async Task CheckNewAuditEquivalentToEntityFromGet()
        {
            var newAudit = _scenarioContext.Get<AuditModel>(AuditRoutesConstants.CreatedAuditKey);

            var response = await _httpClient.GetFromJsonAsync<AuditModel>(AuditRoutesConstants.GetPath + $"?id={newAudit.Id}");

            newAudit.Should().BeEquivalentTo(response);
        }
        public async Task CheckEmptyResponse(string comparisonKey)
        {
            var createdAudit = _scenarioContext.Get<AuditModel>(comparisonKey);

            var clientMessage = await _httpClient.GetAsync(AuditRoutesConstants.GetPath + $"?id={createdAudit.Id}");

            clientMessage.StatusCode.Should().NotBe(System.Net.HttpStatusCode.OK);
        }
        #endregion
    }
}
