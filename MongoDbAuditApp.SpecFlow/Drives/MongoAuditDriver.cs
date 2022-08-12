using MongoDbAuditApp.Specflow.Models;
using MongoDbAuditApp.SpecFlow.Constants;
using MongoDB.Bson;
using System.Net.Http.Json;
using System.Text;
using TechTalk.SpecFlow;

namespace MongoDbAuditApp.SpecFlow.Drives
{
    public class MongoAuditDriver
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;

        public MongoAuditCreateModel RequestModel { get; set; }

        public MongoAuditDriver(HttpClient httpClient, ScenarioContext scenarioContext)
        {
            _httpClient = httpClient;
            _scenarioContext = scenarioContext;
        }

        #region Act Methods
        public async Task PostAudit()
        {
            var response = await _httpClient.PostAsJsonAsync(MongoAuditRoutesConstants.PostPath, RequestModel);

            var newAddedAudit = await response.Content.ReadFromJsonAsync<MongoAuditModel>();

            _scenarioContext.Add(MongoAuditRoutesConstants.CreatedMongoAuditKey, newAddedAudit);
        }


        public async Task<bool> DeleteRecentlyAddedAudit()
        {
            var newAddedAudit = _scenarioContext.Get<MongoAuditModel>(MongoAuditRoutesConstants.CreatedMongoAuditKey);
            var auditDeleteModel = new MongoAuditDeleteModel();
            auditDeleteModel.Id = newAddedAudit.Id.ToString(); 

            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(auditDeleteModel), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(MongoAuditRoutesConstants.DeletePath)
            };

            var response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
        #endregion



        #region Assert Methods
        public async Task CheckNewAuditEquivalentToEntityFromGet()
        {
            var newAudit = _scenarioContext.Get<MongoAuditModel>(MongoAuditRoutesConstants.CreatedMongoAuditKey);

            //serverul mongodb converteste orice variabila datetime in utc datetime
            newAudit.TimeStamp = newAudit.TimeStamp.ToUniversalTime();
            var response = await _httpClient.GetFromJsonAsync<MongoAuditModel>(MongoAuditRoutesConstants.GetPath + $"?id={newAudit.Id}");

            newAudit.Should().BeEquivalentTo(response);
        }
        public async Task CheckEmptyResponse(string comparisonKey)
        {
            var createdAudit = _scenarioContext.Get<MongoAuditModel>(comparisonKey);

            var clientMessage = await _httpClient.GetAsync(MongoAuditRoutesConstants.GetPath + $"?id={createdAudit.Id}");

            clientMessage.StatusCode.Should().NotBe(System.Net.HttpStatusCode.OK);
        }
        #endregion
    }
}
