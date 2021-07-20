using DesignPatternsWithSeleniumWebDriver.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;

namespace DesignPatternsWithSeleniumWebDriver.WebDriver
{
    public class RemoteChromeBrowserDriver : IDriver
    {
        private IWebDriver driver = null;

        public IWebDriver Driver
        {
            get
            {
                if(driver == null)
                {
                    var option = new ChromeOptions();
                    option.AddArgument("disable-infobars");
                    option.AddArgument("--no-sandbox");
                    driver = new RemoteWebDriver(new Uri("http://localhost:5566/wd/hub"), option.ToCapabilities());
                }
                return driver;
            }
        }
    }
}