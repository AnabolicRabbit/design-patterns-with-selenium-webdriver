﻿using DesignPatternsWithSeleniumWebDriver.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace DesignPatternsWithSeleniumWebDriver.WebDriver
{
    public class FirefoxBrowserDriver : IDriver
    {
        private IWebDriver driver = null;

        public IWebDriver Driver
        {
            get
            {
                if (driver == null)
                {
                    var service = FirefoxDriverService.CreateDefaultService();
                    var options = new FirefoxOptions();
                    driver = new FirefoxDriver(service, options);
                }
                return driver;
            }
        }
    }
}