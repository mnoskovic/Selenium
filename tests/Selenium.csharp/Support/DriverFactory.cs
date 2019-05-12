using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;

namespace Selenium.csharp
{
    public static class DriverFactory
    {
        private static IWebDriver CreateChromeDriver()
        {
            var chromeOptions = CreateChromeOptions();
            return new ChromeDriver(chromeOptions);
        }

        private static ChromeOptions CreateChromeOptions()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");
            chromeOptions.AddArgument("--disable-notifications");
            return chromeOptions;
        }

        private static IWebDriver CreateInternetExplorerDriver()
        {
            var internetExplorerOptions = new InternetExplorerOptions();
            internetExplorerOptions.IgnoreZoomLevel = true;
            return new InternetExplorerDriver(internetExplorerOptions);
        }

        private static IWebDriver CreateFirefoxDriver()
        {
            var firefoxOptions = new FirefoxOptions();
            return new FirefoxDriver(firefoxOptions);

        }

        private static IWebDriver CreateRemoteDriver()
        {
            var options = CreateChromeOptions();
            return new RemoteWebDriver(new Uri("http://mnoselv2.westeurope.azurecontainer.io:4444/wd/hub"), options);
        }


        public static IWebDriver Create(string driver)
        {
            switch (driver?.Trim().ToLower())
            {
                case "chrome":
                    return CreateChromeDriver();
                case "ie":
                    return CreateInternetExplorerDriver();
                case "firefox":
                    return CreateFirefoxDriver();
                default:
                    //return CreateChromeDriver();
                    //return CreateInternetExplorerDriver();
                    //return CreateFirefoxDriver();
                    return CreateRemoteDriver();
            }
        }
    }
}
