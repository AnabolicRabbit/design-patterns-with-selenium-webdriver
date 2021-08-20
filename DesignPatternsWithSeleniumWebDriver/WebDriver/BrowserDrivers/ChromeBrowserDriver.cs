using DesignPatternsWithSeleniumWebDriver.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;

namespace DesignPatternsWithSeleniumWebDriver.WebDriver
{
    public class ChromeBrowserDriver : IDriver
    {
        private IWebDriver driver = null;

        public IWebDriver Driver
        {
            get
            {
                if (driver == null)
                {
                    var service = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    var options = new ChromeOptions();
                    options.AddArgument("disable-infobars");
                    driver = new ChromeDriver(service, options);
                }
                return driver;
            }
        }
    }
}