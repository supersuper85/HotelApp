using HotelApp.SpecFlow.Models.ApartmentModels;
using HotelApp.SpecFlow.Constants;
using HotelApp.SpecFlow.Drivers;
using TechTalk.SpecFlow.Assist;

namespace HotelApp.SpecFlow.StepDefinitions
{
    [Binding]
    public class ApartmentStepDefinitions
    {
        private readonly ApartmentDriver _apartmentDriver;

        public ApartmentStepDefinitions(ApartmentDriver apartmentDriver)
        {
            _apartmentDriver = apartmentDriver;
        }

        [Given(@"The apartment")]
        public void GivenTheApartment(Table table)
        {
            var createRequestModel = table.CreateSet<ApartmentCreateModel>().First();
            _apartmentDriver.RequestModel = createRequestModel;
        }

        [When(@"I press create apartment button")]
        public async Task WhenIPressCreateApartmentButton()
        {
            await _apartmentDriver.PostApartment();
        }

        [Then(@"Apartment is created successfully")]
        public async void ThenApartmentIsCreatedSuccessfully()
        {
            await _apartmentDriver.CheckNewApartmentEquivalentToEntityFromGet();
            await _apartmentDriver.DeleteRecentlyAddedApartment();
        }

        [When(@"I press edit apartment button with the following details")]
        public async Task WhenIPressEditApartmentButtonWithTheFollowingDetails(Table table)
        {
            await _apartmentDriver.PostApartment();

            var editRequestModel = table.CreateSet<ApartmentEditModel>().First();

            await _apartmentDriver.EditApartment(editRequestModel);
        }

        [Then(@"Apartment change it's fields with data from edit action")]
        public async Task ThenApartmentChangeItsFieldsWithDataFromEditAction()
        {
            await _apartmentDriver.CheckEditedApartmentEquivalentToEntityFromGet();
            await _apartmentDriver.DeleteRecentlyAddedApartment();
        }

        [When(@"I press delete apartment button")]
        public async Task WhenIPressDelete()
        {
            await _apartmentDriver.PostApartment();
            await _apartmentDriver.DeleteRecentlyAddedApartment();
        }

        [Then(@"Apartment is deleted successfully")]
        public async Task ThenApartmentIsDeletedSuccessfully()
        {
            await _apartmentDriver.CheckEmptyResponse(ApartmentRoutesConstants.CreatedApartmentKey);
        }
    }
}
