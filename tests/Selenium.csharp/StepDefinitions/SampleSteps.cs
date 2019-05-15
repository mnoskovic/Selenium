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

        [When(@"I navigate to Oil products")]
        public void NavigateToOilProducts()
        {
            Page.NavigateToOilProducts();
        }


        [When(@"I login as a user ""(.*)"" with password ""(.*)""")]
        public void Login(string username, string password)
        {
            Page.Login(username, password);
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


        [Then(@"I should see ""(.*)"" product")]
        public void ThenIShouldSeeProduct(string product)
        {
            Assert.True(Page.HasProduct(product));
        }
    }
}
