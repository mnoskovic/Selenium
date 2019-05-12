using System;
using System.Diagnostics;
using TechTalk.SpecFlow;

namespace Selenium.csharp
{
    [Binding]
    public static class TestRunHooks
    {

        [BeforeTestRun]
        public static void Run()
        {
            var args = Environment.GetCommandLineArgs();
            Debug.WriteLine("startup arguments: {0}", string.Join(",", args));
        }

    }
}
