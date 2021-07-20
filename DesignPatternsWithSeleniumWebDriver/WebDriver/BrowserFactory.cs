using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Reflection;

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
                        var service = FirefoxDriverService.CreateDefaultService();
                        var options = new FirefoxOptions();
                        driver = new FirefoxDriver(service, options, TimeSpan.FromSeconds(timeoutSec));
                        break;
                    }
                case BrowsersList.BrowserType.Chrome:
                    {
                        var service = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                        var options = new ChromeOptions();
                        options.AddArgument("disable-infobars");
                        driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(timeoutSec));
                        break;
                    }
                case BrowsersList.BrowserType.remoteFirefox:
                    {
                        var option = new FirefoxOptions();
                        option.AddArgument("disable-infobars");
                        option.AddArgument("--no-sandbox");
                        driver = new RemoteWebDriver(new Uri("http://localhost:6655/wd/hub"), option.ToCapabilities());
                        break;
                    }
                case BrowsersList.BrowserType.remoteChrome:
                    {
                        var option = new ChromeOptions();
                        option.AddArgument("disable-infobars");
                        option.AddArgument("--no-sandbox");
                        driver = new RemoteWebDriver(new Uri("http://localhost:5566/wd/hub"), option.ToCapabilities());
                        break;
                    }
            }

            return driver;
        }
    }
}