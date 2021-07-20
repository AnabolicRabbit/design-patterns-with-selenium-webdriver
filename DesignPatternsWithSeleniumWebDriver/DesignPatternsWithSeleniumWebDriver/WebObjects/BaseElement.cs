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
        protected string elementName;
        protected By locator;
        protected IWebElement element;

        public BaseElement(By pointer)
        {
            locator = pointer;
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
            new WebDriverWait(Browser.Driver, TimeSpan.FromSeconds(5)).Until(condition =>
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

            IJavaScriptExecutor executor = Browser.Driver as IJavaScriptExecutor;
            executor.ExecuteScript("arguments[0].style.background='yellow'", this.GetElement());
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
            Browser.Driver.FindElement(locator).Click();
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
            Browser.Driver.FindElement(locator).Click();
            Browser.Driver.FindElement(locator).Clear();
            IJavaScriptExecutor executor = Browser.Driver as IJavaScriptExecutor;
            executor.ExecuteScript("arguments[0].setAttribute('value',arguments[1]\n)", this.GetElement(), text);
            //executor.ExecuteScript("arguments[0].value=arguments[1]\n", this.GetElement(), text);
            Browser.Driver.FindElement(locator).Click();
        }

        public void Submit()
        {
            throw new NotImplementedException();
        }
    }
}