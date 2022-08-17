using HotelApp.SpecFlow.Models.ApartmentModels;
using HotelApp.SpecFlow.Constants;
using System.Net.Http.Json;
using System.Text;

namespace HotelApp.SpecFlow.Drivers
{
    public class ApartmentDriver
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;

        public ApartmentCreateModel RequestModel { get; set; }

        public ApartmentDriver(HttpClient httpClient, ScenarioContext scenarioContext)
        {
            _httpClient = httpClient;
            _scenarioContext = scenarioContext;
        }

        #region Act Methods
        public async Task PostApartment()
        {
            var response = await _httpClient.PostAsJsonAsync(ApartmentRoutesConstants.PostPath, RequestModel);

            var newAddedApartment = await response.Content.ReadFromJsonAsync<ApartmentModel>();

            _scenarioContext.Add(ApartmentRoutesConstants.CreatedApartmentKey, newAddedApartment);
        }

        public async Task<bool> EditApartment(ApartmentEditModel editRequestModel)
        {
            var mockApartment = _scenarioContext.Get<ApartmentModel>(ApartmentRoutesConstants.CreatedApartmentKey);
            editRequestModel.Id = mockApartment.Id;

            var response = await _httpClient.PutAsJsonAsync(ApartmentRoutesConstants.EditPath, editRequestModel);

            _scenarioContext.Add(ApartmentRoutesConstants.EditedApartmentKey, editRequestModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteRecentlyAddedApartment()
        {
            var recentlyAddedApartment = _scenarioContext.Get<ApartmentModel>(ApartmentRoutesConstants.CreatedApartmentKey);
            var apartmentDeleteModel = new ApartmentDeleteModel();
            apartmentDeleteModel.Id = recentlyAddedApartment.Id;

            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(apartmentDeleteModel), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(ApartmentRoutesConstants.DeletePath)
            };

            var response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
        #endregion



        #region Assert Methods
        public async Task CheckNewApartmentEquivalentToEntityFromGet()
        {
            var newApartment = _scenarioContext.Get<ApartmentModel>(ApartmentRoutesConstants.CreatedApartmentKey);

            var response = await _httpClient.GetFromJsonAsync<ApartmentModel>(ApartmentRoutesConstants.GetPath + $"?id={newApartment.Id}");

            newApartment.Should().BeEquivalentTo(response);
        }
        public async Task CheckEditedApartmentEquivalentToEntityFromGet()
        {
            var apartmentBeforeEditing = _scenarioContext.Get<ApartmentModel>(ApartmentRoutesConstants.CreatedApartmentKey);
            var editedApartment = _scenarioContext.Get<ApartmentEditModel>(ApartmentRoutesConstants.EditedApartmentKey);

            var expectedResponse = new ApartmentModel();
            expectedResponse.Id = editedApartment.Id;
            expectedResponse.DailyRentInEuro = editedApartment.DailyRentInEuro;
            expectedResponse.NumberOfRooms = editedApartment.NumberOfRooms;
            expectedResponse.ApartmentNumber = editedApartment.ApartmentNumber;
            expectedResponse.CustomerId = apartmentBeforeEditing.CustomerId;
            expectedResponse.ReservationId = apartmentBeforeEditing.ReservationId;
            expectedResponse.HotelId = apartmentBeforeEditing.HotelId;

            var response = await _httpClient.GetFromJsonAsync<ApartmentModel>(ApartmentRoutesConstants.GetPath + $"?id={expectedResponse.Id}");

            expectedResponse.Should().BeEquivalentTo(response);
        }
        public async Task CheckEmptyResponse(string comparisonKey)
        {
            var createdApartment = _scenarioContext.Get<ApartmentModel>(comparisonKey);

            var clientMessage = await _httpClient.GetAsync(ApartmentRoutesConstants.GetPath + $"?id={createdApartment.Id}");

            clientMessage.StatusCode.Should().NotBe(System.Net.HttpStatusCode.OK);
        }
        #endregion
    }
}
