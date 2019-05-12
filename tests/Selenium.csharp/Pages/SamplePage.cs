using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Selenium.csharp
{
    [Binding]
    public class SamplePage
    {
        private IDriverProvider Provider;

        public SamplePage(IDriverProvider provider)
        {
            Provider = provider;
        }

        public void NavigateToPage()
        {
            Provider.CurrentDriver.Navigate().GoToUrl("https://google.com");
        }

        public void SearchFor(string text)
        {
            Provider.CurrentWaitDriver.Until((driver)=>driver.FindElement(By.Name("q"))).SendKeys(text + "\n");
        }

        public bool HasTitle(string title)
        {
            return Provider.CurrentWaitDriver.Until((driver)=>driver.Title.Contains(title));
        }
    }
}
