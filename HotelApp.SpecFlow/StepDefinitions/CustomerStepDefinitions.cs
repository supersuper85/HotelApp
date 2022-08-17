using HotelApp.SpecFlow.Constants;
using HotelApp.SpecFlow.Drives;
using HotelApp.SpecFlow.Models.CustomerModels;
using TechTalk.SpecFlow.Assist;

namespace HotelApp.SpecFlow.CustomerStepDefinitions
{
    [Binding]
    public class CustomerStepDefinitions
    {
        private readonly CustomerDriver _customerDriver;

        public CustomerStepDefinitions(CustomerDriver customerDriver)
        {
            _customerDriver = customerDriver;
        }

        [Given(@"The customer")]
        public void GivenTheCustomer(Table table)
        {
            var createRequestModel = table.CreateSet<CustomerCreateModel>().First();
            _customerDriver.RequestModel = createRequestModel;
        }

        [When(@"I press create customer button")]
        public async Task WhenIPressCreateCustomerButton()
        {
            await _customerDriver.PostCustomer();
        }

        [Then(@"Customer is created successfully")]
        public async void ThenCustomerIsCreatedSuccessfully()
        {
            await _customerDriver.CheckNewCustomerEquivalentToEntityFromGet(CustomerRoutesConstants.CreatedCustomerKey);
            await _customerDriver.DeleteRecentlyAddedCustomer();
        }

        [When(@"I press edit customer button with the following details")]
        public async Task WhenIPressEditCustomerButtonWithTheFollowingDetails(Table table)
        {
            await _customerDriver.PostCustomer();

            var editRequestModel = table.CreateSet<CustomerModel>().First();

            await _customerDriver.EditCustomer(editRequestModel);
        }

        [Then(@"Customer change it's fields with data from edit action")]
        public async Task ThenCustomerChangeItsFieldsWithDataFromEditAction()
        {
            await _customerDriver.CheckNewCustomerEquivalentToEntityFromGet(CustomerRoutesConstants.EditedCustomerKey);
            await _customerDriver.DeleteRecentlyAddedCustomer();
        }

        [When(@"I press delete customer button")]
        public async Task WhenIPressDeleteCustomer()
        {
            await _customerDriver.PostCustomer();
            await _customerDriver.DeleteRecentlyAddedCustomer();
        }

        [Then(@"Customer is deleted successfully")]
        public async Task ThenCustomerIsDeletedSuccessfully()
        {
            await _customerDriver.CheckEmptyResponse(CustomerRoutesConstants.CreatedCustomerKey);
        }
    }
}
