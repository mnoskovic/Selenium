using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.csharp
{
    public interface IDriverProvider
    {
        IWebDriver CurrentDriver { get; }

        IWait<IWebDriver> CurrentWaitDriver { get; }

    }
}