using DesignPatternsWithSeleniumWebDriver.Entity;
using DesignPatternsWithSeleniumWebDriver.WebDriver;
using DesignPatternsWithSeleniumWebDriver.WebObjects;
using NUnit.Framework;
using System;

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
            Browser.NavigateTo();
            Browser.MaximizeWindow();

            var login = Convert.ToString(Configuration.Login);
            var password = Convert.ToString(Configuration.Password);

            var user = new User(login, password);

            homePage.GoToSignInForm();
            signInAndOutPage.LoginToEmail(user);
            homePage.ClickEmailButton();
            emailPage.SwitchToEmailPage();
        }

        [TearDown]
        public virtual void CleanTest()
        {
            Browser.Driver.Close();
            Browser.Driver.Quit();
            Browser.Quit();
        }
    }
}