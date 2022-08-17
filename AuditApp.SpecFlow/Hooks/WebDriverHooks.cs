using BoDi;
using TechTalk.SpecFlow;

namespace AuditApp.SpecFlow.Hooks
{
    [Binding]
    public class WebDriverHooks
    {
        private IObjectContainer _objectContainer;
        private HttpClient _httpClient;

        public WebDriverHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void AddHttpClient()
        {
            HttpClient httpClient = new HttpClient();

            _httpClient = httpClient;

            _objectContainer.RegisterInstanceAs(_httpClient);
        }

        [AfterScenario]
        public void CloseHttpClient()
        {
            _objectContainer.Dispose();
        }
    }
}
