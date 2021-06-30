using OpenQA.Selenium;

namespace DesignPatternsWithSeleniumWebDriver.WebObjects
{
    public class SignInAndOutPage : BasePage
    {
        private static readonly By signInForm = By.ClassName("passp-auth-content");

        public SignInAndOutPage() : base(signInForm, "SignIn Page") { }

        //Lambda expression is used for Page Factory pattern as FindsBy is no more used
        BaseElement LoginField => new BaseElement(By.Id("passp-field-login"));
        BaseElement SignInButton => new BaseElement(By.XPath("//button[contains(@class, 'Button2_type_submit')]"));
        BaseElement PasswordField => new BaseElement(By.Id("passp-field-passwd"));
        BaseElement ActualLoginMessage => new BaseElement(By.XPath("//div[@class='passp-auth-screen passp-welcome-page']/h1/span"));

        public void LoginToEmail()
        {
            LoginField.SendKeys("Selenium1Web1Driver@yandex.by");
            SignInButton.Click();
            PasswordField.SendKeys("$elenium789");
            SignInButton.Click();
        }

        public string GetActualLoginMessage()
        {
            return ActualLoginMessage.GetText();
        }
    }
}