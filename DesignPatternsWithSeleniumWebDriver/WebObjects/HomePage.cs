using OpenQA.Selenium;

namespace DesignPatternsWithSeleniumWebDriver.WebObjects
{
    public class HomePage : BasePage
    {
        private static readonly By searchField = By.XPath("//input[@class='input__control input__input mini-suggest__input']");

        public HomePage() : base(searchField, "Home Page") { }

        private readonly BaseElement signInButton = new BaseElement(By.XPath("//a[@class='home-link desk-notif-card__login-new-item desk-notif-card__login-new-item_enter home-link_black_yes home-link_hover_inherit']"));
        private readonly BaseElement emailButton = new BaseElement(By.XPath("//a[@class='home-link desk-notif-card__domik-mail-line home-link_black_yes']"));

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