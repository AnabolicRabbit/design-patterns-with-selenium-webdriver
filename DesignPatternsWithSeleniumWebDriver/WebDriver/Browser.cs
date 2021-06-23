using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace DesignPatternsWithSeleniumWebDriver.WebDriver
{
    public class Browser
    {
        private static IWebDriver driver;
        private static Browser currentInstance;

        public Browser()
        {
            var service = FirefoxDriverService.CreateDefaultService();
            driver = new FirefoxDriver(service);
        }

        public static Browser Instance => currentInstance ?? (currentInstance = new Browser());

        public static void NavigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public static void MaximizeWindow()
        {
            driver.Manage().Window.Maximize();
        }

        public static IWebDriver GetDriver()
        {
            return driver;
        }

        public static void Quit()
        {
            driver.Close();
            driver.Quit();
        }
    }
}