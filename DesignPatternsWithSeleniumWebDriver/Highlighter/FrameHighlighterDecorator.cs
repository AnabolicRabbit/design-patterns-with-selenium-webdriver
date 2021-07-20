using DesignPatternsWithSeleniumWebDriver.WebDriver;
using OpenQA.Selenium;

namespace DesignPatternsWithSeleniumWebDriver.Highlighter
{
    public class FrameHighlighterDecorator : HighlighterDecorator
    {
        public FrameHighlighterDecorator(Highlighter highlighter) : base (highlighter) { }

        public override void HighlightElement(IWebElement element)
        {
            base.HighlightElement(element);

            IJavaScriptExecutor executor = Browser.Driver as IJavaScriptExecutor;
            executor.ExecuteScript("arguments[0].style.border='2px solid red'", element);
        }
    }
}