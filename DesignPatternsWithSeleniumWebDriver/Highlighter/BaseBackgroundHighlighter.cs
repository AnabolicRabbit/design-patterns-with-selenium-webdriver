using DesignPatternsWithSeleniumWebDriver.WebDriver;
using OpenQA.Selenium;

namespace DesignPatternsWithSeleniumWebDriver.Highlighter
{
    public class BaseBackgroundHighlighter : Highlighter
    {
        public override void HighlightElement(IWebElement element)
        {
            IJavaScriptExecutor executor = Browser.Driver as IJavaScriptExecutor;
            executor.ExecuteScript("arguments[0].style.background='yellow'", element);
        }
    }
}