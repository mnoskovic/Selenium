using TechTalk.SpecFlow;
using Xunit;

namespace Selenium.csharp.StepDefinitions
{

    [Binding]
    public class SampleSteps
    {
        private readonly SamplePage Page;

        public SampleSteps(SamplePage page)
        {
            Page = page;
        }

        [When(@"I navigate to site")]
        public void NavigateToSite()
        {
            Page.NavigateToPage();
        }

        [When(@"I search for ""(.*)""")]
        public void WhenISearchFor(string text)
        {
            Page.SearchFor(text);
        }
        
        [Then(@"I should see on title ""(.*)""")]
        public void ThenIShouldSeeOnTitle(string title)
        {
            Assert.True(Page.HasTitle(title));
        }
    }
}
