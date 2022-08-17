using HotelApp.SpecFlow.Constants;
using HotelApp.SpecFlow.Models.CustomerModels;
using System.Net.Http.Json;
using System.Text;

namespace HotelApp.SpecFlow.Drives
{
    public class CustomerDriver
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;

        public CustomerCreateModel RequestModel { get; set; }

        public CustomerDriver(HttpClient httpClient, ScenarioContext scenarioContext)
        {
            _httpClient = httpClient;
            _scenarioContext = scenarioContext;
        }

        #region Act Methods
        public async Task PostCustomer()
        {
            var response = await _httpClient.PostAsJsonAsync(CustomerRoutesConstants.PostPath, RequestModel);

            var newAddedCustomer = await response.Content.ReadFromJsonAsync<CustomerModel>();

            _scenarioContext.Add(CustomerRoutesConstants.CreatedCustomerKey, newAddedCustomer);
        }

        public async Task<bool> EditCustomer(CustomerModel editRequestModel)
        {
            var mockCustomer = _scenarioContext.Get<CustomerModel>(CustomerRoutesConstants.CreatedCustomerKey);
            editRequestModel.Id = mockCustomer.Id;

            var response = await _httpClient.PutAsJsonAsync(CustomerRoutesConstants.EditPath, editRequestModel);

            _scenarioContext.Add(CustomerRoutesConstants.EditedCustomerKey, editRequestModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteRecentlyAddedCustomer()
        {
            var recentlyAddedCustomer = _scenarioContext.Get<CustomerModel>(CustomerRoutesConstants.CreatedCustomerKey);
            var customerDeleteModel = new CustomerDeleteModel();
            customerDeleteModel.Id = recentlyAddedCustomer.Id;

            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(customerDeleteModel), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(CustomerRoutesConstants.DeletePath)
            };

            var response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
        #endregion



        #region Assert Methods
        public async Task CheckNewCustomerEquivalentToEntityFromGet(string comparisonKey)
        {
            var newCustomer = _scenarioContext.Get<CustomerModel>(comparisonKey);

            var response = await _httpClient.GetFromJsonAsync<CustomerModel>(CustomerRoutesConstants.GetPath + $"?id={newCustomer.Id}");

            newCustomer.Should().BeEquivalentTo(response);
        }
        public async Task CheckEmptyResponse(string comparisonKey)
        {
            var createdCustomer = _scenarioContext.Get<CustomerModel>(comparisonKey);

            var clientMessage = await _httpClient.GetAsync(CustomerRoutesConstants.GetPath + $"?id={createdCustomer.Id}");

            clientMessage.StatusCode.Should().NotBe(System.Net.HttpStatusCode.OK);
        }
        #endregion
    }
}
