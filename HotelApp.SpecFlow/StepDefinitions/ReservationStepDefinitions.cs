using HotelApp.SpecFlow.Constants;
using HotelApp.SpecFlow.Drivers;
using HotelApp.SpecFlow.Models.ReservationModels;
using TechTalk.SpecFlow.Assist;

namespace HotelApp.SpecFlow.StepDefinitions
{
    [Binding]
    public class ReservationStepDefinitions
    {
        private readonly ReservationDriver _reservationDriver;

        public ReservationStepDefinitions(ReservationDriver reservationDriver)
        {
            _reservationDriver = reservationDriver;
        }

        [Given(@"The reservation")]
        public void GivenTheReservation(Table table)
        {
            var createRequestModel = table.CreateSet<ReservationCreateModel>().First();
            _reservationDriver.RequestModel = createRequestModel;
        }
        [Given(@"The customer Id of reservation")]
        public void GivenTheCustomerModelOfReservation(Table table)
        {
            var customerModel = table.CreateSet<ReservationCustomerCreateModel>().First();
            _reservationDriver.RequestModel.Customer = customerModel;
        }

        [When(@"I press create reservation button")]
        public async Task WhenIPressCreateReservationButton()
        {
            await _reservationDriver.PostReservation();
        }

        [Then(@"Reservation is created successfully")]
        public async void ThenReservationIsCreatedSuccessfully()
        {
            await _reservationDriver.CheckNewReservationEquivalentToEntityFromGet();
            await _reservationDriver.DeleteRecentlyAddedReservation();
        }

        [When(@"I press edit reservation button with the following details")]
        public async Task WhenIPressEditReservationButtonWithTheFollowingDetails(Table table)
        {
            await _reservationDriver.PostReservation();

            var editRequestModel = table.CreateSet<ReservationEditModel>().First();

            await _reservationDriver.EditReservation(editRequestModel);
        }

        [Then(@"Reservation change it's fields with data from edit action")]
        public async Task ThenReservationChangeItsFieldsWithDataFromEditAction()
        {
            await _reservationDriver.CheckEditedReservationEquivalentToEntityFromGet();
            await _reservationDriver.DeleteRecentlyAddedReservation();
        }

        [When(@"I press delete reservation button")]
        public async Task WhenIPressDeleteReservation()
        {
            await _reservationDriver.PostReservation();
            await _reservationDriver.DeleteRecentlyAddedReservation();
        }

        [Then(@"Reservation is deleted successfully")]
        public async Task ThenReservationIsDeletedSuccessfully()
        {
            await _reservationDriver.CheckEmptyResponse(ReservationRoutesConstants.CreatedReservationKey);
        }
    }
}
