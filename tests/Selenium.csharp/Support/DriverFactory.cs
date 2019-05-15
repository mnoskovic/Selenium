using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Configuration;

namespace Selenium.csharp
{
    public static class DriverFactory
    {
        private static ChromeOptions CreateChromeOptions()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");
            chromeOptions.AddArgument("--disable-notifications");
            return chromeOptions;
        }

        private static IWebDriver CreateRemoteDriver()
        {
            var options = CreateChromeOptions();
            return new RemoteWebDriver(new Uri(ConfigurationManager.AppSettings["selenium"]), options);
        }


        public static IWebDriver Create()
        {
            return CreateRemoteDriver();
        }
    }
}
