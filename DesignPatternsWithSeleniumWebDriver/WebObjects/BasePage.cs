using OpenQA.Selenium;

namespace DesignPatternsWithSeleniumWebDriver.WebObjects
{
    public abstract class BasePage
    {
        protected By locator;
        protected string title;
        public static string titleForm;

        protected BasePage(By titleLocator, string elementTitle)
        {
            locator = titleLocator;
            title = titleForm = elementTitle;
        }
    }
}