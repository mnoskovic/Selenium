using OpenQA.Selenium;
using System.Configuration;
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
            Provider.CurrentDriver.Navigate().GoToUrl(ConfigurationManager.AppSettings["url"]);
        }

        public void NavigateToOilProducts()
        {
            Provider.CurrentWaitDriver.Until((driver) => driver.FindElement(By.PartialLinkText("Oil"))).Click();
        }

        public void Login(string username, string password)
        {
            Provider.CurrentWaitDriver.Until((driver) => driver.FindElement(By.Id("login-link"))).Click();
            Provider.CurrentWaitDriver.Until((driver) => driver.FindElement(By.Id("Email"))).SendKeys(username + "\n");
            Provider.CurrentWaitDriver.Until((driver) => driver.FindElement(By.Id("Password"))).SendKeys(password + "\n");

        }


        public void SearchFor(string text)
        {
            Provider.CurrentWaitDriver.Until((driver)=>driver.FindElement(By.Name("q"))).SendKeys(text + "\n");
        }

        public bool HasTitle(string title)
        {
            return Provider.CurrentWaitDriver.Until((driver)=>driver.Title.Contains(title));
        }

        public bool HasProduct(string product)
        {
            return Provider.CurrentWaitDriver.Until((driver) => driver.FindElement(By.XPath($"//a[contains(@title, '{product}')]"))) != null;

        }

    }
}
