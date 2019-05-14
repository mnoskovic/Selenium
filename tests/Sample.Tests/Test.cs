using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;

namespace Sample.Tests
{
    public class Test : IDisposable
    {
        private IWebDriver WebDriver;
        private WebDriverWait WebDriverWait;

        public Test()
        {
            var driver = WebDriverFactory.Create();
            WebDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            WebDriver = driver;

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                WebDriver.Dispose();
            }
        }

        [Fact]
        public void Run()
        {
            WebDriver.Navigate().GoToUrl("https://www.google.com");
            WebDriverWait.Until(d => d.FindElement(By.Name("q"))).SendKeys("Accenture\n");

            var validTitle = WebDriverWait.Until(d => d.Title.StartsWith("accenture", StringComparison.OrdinalIgnoreCase));
            Assert.True(validTitle);
        }
    }

    public class HeadlessTest : IDisposable
    {
        private IWebDriver WebDriver;
        private WebDriverWait WebDriverWait;

        public HeadlessTest()
        {
            var driver = WebDriverFactory.Create(headless: true);
            WebDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            WebDriver = driver;

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                WebDriver.Dispose();
            }
        }

        [Fact]
        public void Run()
        {
            WebDriver.Navigate().GoToUrl("https://www.google.com");
            WebDriverWait.Until(d => d.FindElement(By.Name("q"))).SendKeys("Accenture\n");

            var validTitle = WebDriverWait.Until(d => d.Title.StartsWith("accenture", StringComparison.OrdinalIgnoreCase));
            Assert.True(validTitle);
        }
    }
}
