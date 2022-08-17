using HotelApp.SpecFlow.Constants;
using HotelApp.SpecFlow.Models.ReservationModels;
using System.Net.Http.Json;
using System.Text;

namespace HotelApp.SpecFlow.Drivers
{
    public class ReservationDriver
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;

        public ReservationCreateModel RequestModel { get; set; }

        public ReservationDriver(HttpClient httpClient, ScenarioContext scenarioContext)
        {
            _httpClient = httpClient;
            _scenarioContext = scenarioContext;
        }

        #region Act Methods
        public async Task PostReservation()
        {
            var response = await _httpClient.PostAsJsonAsync(ReservationRoutesConstants.PostPath, RequestModel);

            var newAddedReservation = await response.Content.ReadFromJsonAsync<ReservationModel>();

            _scenarioContext.Add(ReservationRoutesConstants.CreatedReservationKey, newAddedReservation);
        }

        public async Task<bool> EditReservation(ReservationEditModel editRequestModel)
        {
            var mockReservation = _scenarioContext.Get<ReservationModel>(ReservationRoutesConstants.CreatedReservationKey);
            editRequestModel.Id = mockReservation.Id;

            var response = await _httpClient.PutAsJsonAsync(ReservationRoutesConstants.EditPath, editRequestModel);

            _scenarioContext.Add(ReservationRoutesConstants.EditedReservationKey, editRequestModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteRecentlyAddedReservation()
        {
            var recentlyAddedReservation = _scenarioContext.Get<ReservationModel>(ReservationRoutesConstants.CreatedReservationKey);
            var reservationDeleteModel = new ReservationDeleteModel();
            reservationDeleteModel.Id = recentlyAddedReservation.Id;

            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(reservationDeleteModel), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(ReservationRoutesConstants.DeletePath)
            };

            var response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
        #endregion



        #region Assert Methods
        public async Task CheckNewReservationEquivalentToEntityFromGet()
        {
            var newReservation = _scenarioContext.Get<ReservationModel>(ReservationRoutesConstants.CreatedReservationKey);

            var response = await _httpClient.GetFromJsonAsync<ReservationModel>(ReservationRoutesConstants.GetPath + $"?id={newReservation.Id}");

            newReservation.CustomerId.Should().Be(response.Customer.Id);
        }
        public async Task CheckEditedReservationEquivalentToEntityFromGet()
        {
            var newReservation = _scenarioContext.Get<ReservationEditModel>(ReservationRoutesConstants.EditedReservationKey);

            var response = await _httpClient.GetFromJsonAsync<ReservationModel>(ReservationRoutesConstants.GetPath + $"?id={newReservation.Id}");

            newReservation.ReleaseDate.Should().Be(response.ReleaseDate);
        }
        public async Task CheckEmptyResponse(string comparisonKey)
        {
            var createdReservation = _scenarioContext.Get<ReservationModel>(comparisonKey);

            var clientMessage = await _httpClient.GetAsync(ReservationRoutesConstants.GetPath + $"?id={createdReservation.Id}");

            clientMessage.StatusCode.Should().NotBe(System.Net.HttpStatusCode.OK);
        }
        #endregion
    }
}
