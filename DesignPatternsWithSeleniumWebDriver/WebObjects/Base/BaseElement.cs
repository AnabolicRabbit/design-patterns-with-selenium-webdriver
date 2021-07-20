using DesignPatternsWithSeleniumWebDriver.Highlighter;
using DesignPatternsWithSeleniumWebDriver.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace DesignPatternsWithSeleniumWebDriver.WebObjects
{
    public class BaseElement : IWebElement
    {
        protected By locator;
        protected IWebElement element;

        private static readonly By emailLogo = By.ClassName("PSHeaderLogo360-360");

        public BaseElement(By pointer)
        {
            locator = pointer;
        }

        public BaseElement()
        {
        }

        public string GetText()
        {
            WaitForIsVisible();
            return Browser.Driver.FindElement(locator).Text;
        }

        public IWebElement GetElement()
        {
            try
            {
                element = Browser.Driver.FindElement(locator);
            }
            catch (Exception)
            {
                throw;
            }
            return element;
        }

        public void WaitForIsVisible()
        {
            new WebDriverWait(Browser.Driver, TimeSpan.FromSeconds(30)).Until(condition =>
            {
                try
                {
                    var elementToBeDisplayed = Browser.Driver.FindElement(locator);
                    return elementToBeDisplayed.Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });

            try
            {
                Browser.Driver.FindElement(emailLogo);

                BaseBackgroundHighlighter highlighter = new BaseBackgroundHighlighter();
                FrameHighlighterDecorator decorator = new FrameHighlighterDecorator(highlighter);
                decorator.HighlightElement(this.GetElement());
            }
            catch
            {
                BaseBackgroundHighlighter highlighter = new BaseBackgroundHighlighter();
                highlighter.HighlightElement(this.GetElement());
            }

        }

        public string TagName => throw new NotImplementedException();

        public string Text => throw new NotImplementedException();

        public bool Enabled => throw new NotImplementedException();

        public bool Selected => throw new NotImplementedException();

        public Point Location => throw new NotImplementedException();

        public Size Size => throw new NotImplementedException();

        public bool Displayed => throw new NotImplementedException();

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Click()
        {
            WaitForIsVisible();
            GetElement().Click();
        }

        public void JsClick()
        {
            WaitForIsVisible();
            IJavaScriptExecutor executor = Browser.Driver as IJavaScriptExecutor;
            executor.ExecuteScript("arguments[0].click();", this.GetElement());
        }

        public IWebElement FindElement(By by)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            throw new NotImplementedException();
        }

        public string GetAttribute(string attributeName)
        {
            throw new NotImplementedException();
        }

        public string GetCssValue(string propertyName)
        {
            throw new NotImplementedException();
        }

        public string GetProperty(string propertyName)
        {
            throw new NotImplementedException();
        }

        public void SendKeys(string text)
        {
            WaitForIsVisible();
            Browser.Driver.FindElement(locator).SendKeys(text);
        }

        public void JsSendKeys(string text)
        {
            WaitForIsVisible();
            IJavaScriptExecutor executor = Browser.Driver as IJavaScriptExecutor;
            executor.ExecuteScript("arguments[0].setAttribute('value',arguments[1]\n)", this.GetElement(), text);
        }

        public void Submit()
        {
            throw new NotImplementedException();
        }
    }
}