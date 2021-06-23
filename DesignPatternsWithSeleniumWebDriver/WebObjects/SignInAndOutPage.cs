using OpenQA.Selenium;

namespace DesignPatternsWithSeleniumWebDriver.WebObjects
{
    public class SignInAndOutPage : BasePage
    {
        private static readonly By signInForm = By.ClassName("passp-auth-content");

        public SignInAndOutPage() : base(signInForm, "SignIn Page") { }

        //Lambda expression is used for Page Factory pattern as FindsBy is no more used
        BaseElement loginField => new BaseElement(By.Id("passp-field-login"));
        BaseElement signInButton => new BaseElement(By.XPath("//button[@class='Button2 Button2_size_l Button2_view_action Button2_width_max Button2_type_submit']"));
        BaseElement passwordField => new BaseElement(By.Id("passp-field-passwd"));
        BaseElement actualLoginMessage => new BaseElement(By.XPath("//div[@class='passp-auth-screen passp-welcome-page']/h1/span"));

        public void LoginToEmail()
        {
            loginField.SendKeys("Selenium1Web1Driver@yandex.by");
            signInButton.Click();
            passwordField.SendKeys("$elenium789");
            signInButton.Click();
        }

        public string GetActualLoginMessage()
        {
            return actualLoginMessage.GetText();
        }
    }
}