using OpenQA.Selenium;

namespace DesignPatternsWithSeleniumWebDriver.WebObjects
{
    public abstract class BasePage
    {
        protected By titleLocator;
        protected string title;

        protected BasePage(By titleLocator, string titleName)
        {
            this.titleLocator = titleLocator;
            title = titleName;
        }
    }
}