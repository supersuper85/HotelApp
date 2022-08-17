using AuditApp.Specflow.Models;
using AuditApp.SpecFlow.Constants;
using AuditApp.SpecFlow.Drives;
using TechTalk.SpecFlow.Assist;

namespace MongoDbAuditApp.SpecFlow.StepDefinitions
{
    [Binding]
    public class AuditStepDefinitions
    {
        private readonly AuditDriver _auditDriver;

        public AuditStepDefinitions(AuditDriver customerDriver)
        {
            _auditDriver = customerDriver;
        }

        [Given(@"The audit")]
        public void GivenTheAudit(Table table)
        {
            var createRequestModel = table.CreateSet<AuditCreateModel>().First();
            _auditDriver.RequestModel = createRequestModel;
        }

        [When(@"I press create audit button")]
        public async Task WhenIPressCreateAuditButton()
        {
            await _auditDriver.PostAudit();
        }

        [Then(@"Audit is created successfully")]
        public async void ThenAuditIsCreatedSuccessfully()
        {
            await _auditDriver.CheckNewAuditEquivalentToEntityFromGet();
            await _auditDriver.DeleteRecentlyAddedAudit();
        }

        
    }
}
