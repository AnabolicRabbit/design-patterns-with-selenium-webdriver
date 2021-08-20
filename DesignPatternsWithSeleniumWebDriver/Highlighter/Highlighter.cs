using OpenQA.Selenium;

namespace DesignPatternsWithSeleniumWebDriver.Highlighter
{
    public abstract class Highlighter
    {
        public abstract void HighlightElement(IWebElement element);
    }
}