using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.IO;

namespace Sample.Tests
{
    internal class WebDriverFactory
    {
        public static IWebDriver Create(bool headless = false, bool local = true)
        {

#if DEBUG

            ChromeOptions options = new ChromeOptions();
            if (headless)
            {
                options.AddArgument("--headless");
            }

            if (local)
            {
                return new RemoteWebDriver(options);
            }

            var str = File.ReadAllText("local.settings.json");
            using (var jsonReader = new JsonTextReader(new StringReader(str)))
            {
                var obj = JObject.Load(jsonReader);
                var userName = obj["Values"]["PROVIDER_USERNAME"].Value<string>();
                var accessKey = obj["Values"]["PROVIDER_ACCESSKEY"].Value<string>();
                var url = obj["Values"]["PROVIDER_URL"].Value<string>();

                options.AddAdditionalCapability("username", userName, true);
                options.AddAdditionalCapability("accessKey", accessKey, true);

                return new RemoteWebDriver(new Uri(url), options.ToCapabilities(), TimeSpan.FromSeconds(600));
            }


#else
            var userName = Environment.GetEnvironmentVariable("PROVIDER_USERNAME", EnvironmentVariableTarget.Process);
            var accessKey = Environment.GetEnvironmentVariable("PROVIDER_ACCESSKEY", EnvironmentVariableTarget.Process);
            var url = Environment.GetEnvironmentVariable("PROVIDER_URL", EnvironmentVariableTarget.Process);
            
            ChromeOptions options = new ChromeOptions();
            options.AddAdditionalCapability("username", userName, true);
            options.AddAdditionalCapability("accessKey", accessKey, true);

            return new RemoteWebDriver(new Uri(url), options.ToCapabilities(), TimeSpan.FromSeconds(600));
            
#endif

        }
    }
}
