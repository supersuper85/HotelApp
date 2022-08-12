using MongoDbAuditApp.Specflow.Models;
using MongoDbAuditApp.SpecFlow.Constants;
using MongoDbAuditApp.SpecFlow.Drives;
using TechTalk.SpecFlow.Assist;

namespace MongoDbAuditApp.SpecFlow.StepDefinitions
{
    [Binding]
    public class MongoAuditStepDefinitions
    {
        private readonly MongoAuditDriver _auditDriver;

        public MongoAuditStepDefinitions(MongoAuditDriver auditDriver)
        {
            _auditDriver = auditDriver;
        }

        [Given(@"The mongo audit")]
        public void GivenTheAudit(Table table)
        {
            var createRequestModel = table.CreateSet<MongoAuditCreateModel>().First();
            _auditDriver.RequestModel = createRequestModel;
        }

        [When(@"I press create mongo audit button")]
        public async Task WhenIPressCreateAuditButton()
        {
            await _auditDriver.PostAudit();
        }

        [Then(@"Mongo audit is created successfully")]
        public async void ThenAuditIsCreatedSuccessfully()
        {
            await _auditDriver.CheckNewAuditEquivalentToEntityFromGet();
            await _auditDriver.DeleteRecentlyAddedAudit();
        }

        
    }
}
