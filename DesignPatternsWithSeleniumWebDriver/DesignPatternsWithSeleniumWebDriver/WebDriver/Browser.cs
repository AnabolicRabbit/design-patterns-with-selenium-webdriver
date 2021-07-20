using OpenQA.Selenium;
using System;

namespace DesignPatternsWithSeleniumWebDriver.WebDriver
{
    public class Browser
    {
        public static IWebDriver Driver { get; private set; }
        private static Browser currentInstance;
        private static string browser;
        public static BrowsersList.BrowserType currentBrowser;
        public static int implWait;
        public static double timeoutForElement;

        private Browser()
        {
            InitParams();
            Driver = BrowserFactory.GetDriver(currentBrowser, implWait);
        }

        private static void InitParams()
        {
            implWait = Convert.ToInt32(Configuration.ElementTimeout);
            timeoutForElement = Convert.ToDouble(Configuration.ElementTimeout);
            browser = Configuration.Browser;
            BrowsersList.BrowserType.TryParse(browser, out currentBrowser);
        }

        public static Browser Instance => currentInstance ?? (currentInstance = new Browser());

        public static void NavigateTo()
        {
            IJavaScriptExecutor executor = Browser.Driver as IJavaScriptExecutor;
            executor.ExecuteScript("window.location=arguments[0]", Configuration.StartUrl);
        }

        public static void MaximizeWindow()
        {
            Driver.Manage().Window.Maximize();
        }

        public static void Quit()
        {
            Driver = null;
            currentInstance = null;
            browser = null;
        }
    }
}