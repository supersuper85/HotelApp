using MongoAuditApp.Specflow.Models;
using MongoAuditApp.SpecFlow.Constants;
using MongoAuditApp.SpecFlow.Drives;
using TechTalk.SpecFlow.Assist;

namespace MongoAuditApp.SpecFlow.StepDefinitions
{
    [Binding]
    public class MongoAuditStepDefinitions
    {
        private readonly MongoAuditDriver _customerDriver;

        public MongoAuditStepDefinitions(MongoAuditDriver customerDriver)
        {
            _customerDriver = customerDriver;
        }

        [Given(@"The mongo audit")]
        public void GivenTheCustomer(Table table)
        {
            var createRequestModel = table.CreateSet<MongoAuditCreateModel>().First();
            _customerDriver.RequestModel = createRequestModel;
        }

        [When(@"I press create mongo audit button")]
        public async Task WhenIPressCreateButton()
        {
            await _customerDriver.PostCustomer();
        }

        [Then(@"Mongo audit is created successfully")]
        public async void ThenAddressIsCreatedSuccessfully()
        {
            await _customerDriver.CheckNewCustomerEquivalentToEntityFromGet(MongoAuditRoutesConstants.CreatedMongoAuditKey);
            await _customerDriver.DeleteRecentlyAddedCustomer();
        }

        

        [When(@"I press delete customer button")]
        public async Task WhenIPressDelete()
        {
            await _customerDriver.PostCustomer();
            await _customerDriver.DeleteRecentlyAddedCustomer();
        }

        [Then(@"Customer is deleted successfully")]
        public async Task ThenAddressIsDeletedSuccessfully()
        {
            await _customerDriver.CheckEmptyResponse(MongoAuditRoutesConstants.CreatedMongoAuditKey);
        }
    }
}
