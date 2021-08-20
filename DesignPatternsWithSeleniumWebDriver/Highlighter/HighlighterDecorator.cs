using OpenQA.Selenium;

namespace DesignPatternsWithSeleniumWebDriver.Highlighter
{
    public abstract class HighlighterDecorator : Highlighter
    {
        protected Highlighter highlighter;

        public HighlighterDecorator(Highlighter highlighter)
        {
            this.highlighter = highlighter;
        }

        public void SetHighlighter(Highlighter highlighter)
        {
            this.highlighter = highlighter;
        }

        public override void HighlightElement(IWebElement element)
        {
            if (this.highlighter != null)
            this.highlighter.HighlightElement(element);
        }
    }
}