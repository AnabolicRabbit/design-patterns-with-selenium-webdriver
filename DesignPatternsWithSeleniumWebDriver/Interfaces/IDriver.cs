using OpenQA.Selenium;

namespace DesignPatternsWithSeleniumWebDriver.Utilities
{
    public interface IDriver
    {
        IWebDriver Driver { get; }
    }
}