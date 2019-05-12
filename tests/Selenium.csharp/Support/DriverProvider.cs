using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Selenium.csharp
{
    public class DriverProvider : IDriverProvider, IDriverManager
    {
        private static readonly TimeSpan MaxTimeout = TimeSpan.FromMinutes(1);

        public void Initialize()
        {
            var driverName = Environment.GetEnvironmentVariable("driver");

            var driver = DriverFactory.Create(driverName);
            driver.Manage().Timeouts().ImplicitWait = MaxTimeout;
            CurrentDriver = driver;
        }

        public IWebDriver CurrentDriver { get; private set; }

        public IWait<IWebDriver> CurrentWaitDriver => new WebDriverWait(CurrentDriver, MaxTimeout);

        public void Close()
        {
            if (CurrentDriver != null)
            {
                CurrentDriver.Close();
                CurrentDriver.Dispose();
                CurrentDriver = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
        }
    }
}
