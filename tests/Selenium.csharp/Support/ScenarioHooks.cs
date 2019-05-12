using TechTalk.SpecFlow;

namespace Selenium.csharp
{
    [Binding]
    public class ScenarioHooks
    {
        private readonly IDriverManager Manager;

        public ScenarioHooks(IDriverManager manager)
        {
            Manager = manager;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            Manager.Initialize();

        }

        [AfterScenario]
        public void AfterScenario()
        {
            Manager.Close();
        }
    }
}
