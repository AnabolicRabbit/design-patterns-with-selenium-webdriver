using DesignPatternsWithSeleniumWebDriver.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;

namespace DesignPatternsWithSeleniumWebDriver.WebDriver
{
    public class RemoteFirefoxBrowserDriver : IDriver
    {
        private IWebDriver driver = null;

        public IWebDriver Driver
        {
            get
            {
                if (driver == null)
                {
                    var option = new FirefoxOptions();
                    option.AddArgument("disable-infobars");
                    option.AddArgument("--no-sandbox");
                    driver = new RemoteWebDriver(new Uri("http://localhost:6655/wd/hub"), option.ToCapabilities());
                }
                return driver;
            }
        }
    }
}