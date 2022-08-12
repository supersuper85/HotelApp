using MongoAuditApp.Specflow.Models;
using MongoAuditApp.SpecFlow.Constants;
using MongoDB.Bson;
using System.Net.Http.Json;
using System.Text;

namespace MongoAuditApp.SpecFlow.Drives
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
        public async Task PostCustomer()
        {
            var response = await _httpClient.PostAsJsonAsync(MongoAuditRoutesConstants.PostPath, RequestModel);

            var newAddedCustomer = await response.Content.ReadFromJsonAsync<MongoAuditModel>();

            _scenarioContext.Add(MongoAuditRoutesConstants.CreatedMongoAuditKey, newAddedCustomer);
        }


        public async Task<bool> DeleteRecentlyAddedCustomer()
        {
            var recentlyAddedCustomer = _scenarioContext.Get<MongoAuditModel>(MongoAuditRoutesConstants.CreatedMongoAuditKey);
            var customerDeleteModel = new MongoAuditDeleteModel();
            customerDeleteModel.Id = recentlyAddedCustomer.Id.ToString(); 

            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(customerDeleteModel), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(MongoAuditRoutesConstants.DeletePath)
            };

            var response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
        #endregion



        #region Assert Methods
        public async Task CheckNewCustomerEquivalentToEntityFromGet(string comparisonKey)
        {
            var newCustomer = _scenarioContext.Get<MongoAuditModel>(comparisonKey);

            var response = await _httpClient.GetFromJsonAsync<MongoAuditModel>(MongoAuditRoutesConstants.GetPath + $"?id={newCustomer.Id}");

            newCustomer.Should().BeEquivalentTo(response);
        }
        public async Task CheckEmptyResponse(string comparisonKey)
        {
            var createdCustomer = _scenarioContext.Get<MongoAuditModel>(comparisonKey);

            var clientMessage = await _httpClient.GetAsync(MongoAuditRoutesConstants.GetPath + $"?id={createdCustomer.Id}");

            clientMessage.StatusCode.Should().NotBe(System.Net.HttpStatusCode.OK);
        }
        #endregion
    }
}
