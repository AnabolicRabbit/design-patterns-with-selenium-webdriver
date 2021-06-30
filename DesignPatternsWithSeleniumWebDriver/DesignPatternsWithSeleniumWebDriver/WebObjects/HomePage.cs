using OpenQA.Selenium;

namespace DesignPatternsWithSeleniumWebDriver.WebObjects
{
    public class HomePage : BasePage
    {
        private static readonly By searchField = By.XPath("//input[contains(@class, 'mini-suggest__input')]");

        public HomePage() : base(searchField, "Home Page") { }

        private readonly BaseElement signInButton = new BaseElement(By.XPath("//a[contains(@class, 'desk-notif-card__login-new-item_enter')]"));
        private readonly BaseElement emailButton = new BaseElement(By.XPath("//a[contains(@class, 'desk-notif-card__domik-mail-line')]"));

        public void GoToSignInForm()
        {
            signInButton.Click();
        }

        public void ClickEmailButton()
        {
            emailButton.Click();
        }
    }
}