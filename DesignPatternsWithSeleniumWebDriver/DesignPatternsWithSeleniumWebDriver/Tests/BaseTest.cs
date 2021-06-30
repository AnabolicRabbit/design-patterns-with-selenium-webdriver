using DesignPatternsWithSeleniumWebDriver.WebDriver;
using DesignPatternsWithSeleniumWebDriver.WebObjects;
using NUnit.Framework;

namespace DesignPatternsWithSeleniumWebDriver
{
    public abstract class BaseTest
    {
        protected Browser browser = Browser.Instance;

        private readonly HomePage homePage = new HomePage();
        private readonly SignInAndOutPage signInAndOutPage = new SignInAndOutPage();
        private readonly EmailPage emailPage = new EmailPage();

        [SetUp]
        public virtual void InitializeTest()
        {
            browser = Browser.Instance;
            Browser.NavigateTo("https://yandex.by/");
            Browser.MaximizeWindow();

            homePage.GoToSignInForm();
            signInAndOutPage.LoginToEmail();
            homePage.ClickEmailButton();
            emailPage.SwitchToEmailPage();
        }

        [TearDown]
        public virtual void CleanTest()
        {
            Browser.Driver.Close();
            Browser.Driver.Quit();
        }
    }
}