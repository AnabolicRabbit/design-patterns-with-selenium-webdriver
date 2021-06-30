using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace DesignPatternsWithSeleniumWebDriver.WebDriver
{
    public class Browser
    {
        public static IWebDriver Driver { get; private set; }
        private static Browser currentInstance;

        private Browser()
        {
            var service = FirefoxDriverService.CreateDefaultService();
            Driver = new FirefoxDriver(service);
        }

        public static Browser Instance => currentInstance ?? (currentInstance = new Browser());

        public static void NavigateTo(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public static void MaximizeWindow()
        {
            Driver.Manage().Window.Maximize();
        }
    }
}