using OpenQA.Selenium;

namespace DesignPatternsWithSeleniumWebDriver.WebDriver
{
    public class BrowserFactory
    {
        public static IWebDriver GetDriver(BrowsersList.BrowserType type, int timeoutSec)
        {
            IWebDriver driver = null;

            switch (type)
            {
                case BrowsersList.BrowserType.Firefox:
                    {
                        driver = new FirefoxBrowserDriver().Driver;
                        break;
                    }
                case BrowsersList.BrowserType.Chrome:
                    {
                        driver = new ChromeBrowserDriver().Driver;
                        break;
                    }
                case BrowsersList.BrowserType.remoteFirefox:
                    {
                        driver = new RemoteFirefoxBrowserDriver().Driver;
                        break;
                    }
                case BrowsersList.BrowserType.remoteChrome:
                    {
                        driver = new RemoteChromeBrowserDriver().Driver;
                        break;
                    }
            }
            return driver;
        }
    }
}