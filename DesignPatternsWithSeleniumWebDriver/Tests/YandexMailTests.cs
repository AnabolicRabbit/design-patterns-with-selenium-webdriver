using DesignPatternsWithSeleniumWebDriver.WebObjects;
using NUnit.Framework;
using System;

namespace DesignPatternsWithSeleniumWebDriver
{
    [TestFixture]
    public class YandexMailTests : BaseTest
    {
        private readonly HomePage homePage = new HomePage();
        private readonly SignInAndOutPage signInAndOutPage = new SignInAndOutPage();
        private readonly EmailPage emailPage = new EmailPage();

        [Test]
        public void GetLoggedInUser()
        {
            //Arrange
            homePage.GoToSignInForm();
            signInAndOutPage.LoginToEmail();
            homePage.ClickEmailButton();
            emailPage.SwitchToEmailPage();

            string expectedUserName = "Selenium1Web1Driver";

            //Act
            var actualUserName = emailPage.GetActualUserName();

            //Assert
            Assert.AreEqual(expectedUserName, actualUserName, "The actual logged in user differs from the expected.");
        }

        [Test]
        public void IsPresentInDraftEmail()
        {
            //Arrange
            homePage.GoToSignInForm();
            signInAndOutPage.LoginToEmail();
            homePage.ClickEmailButton();
            emailPage.SwitchToEmailPage();
            emailPage.CreateDraftEmail();

            string expectedSubject = "Greeting";

            //Act
            var actualSubject = emailPage.GetActualSubject();

            //Assert
            Assert.AreEqual(expectedSubject, actualSubject, "The actual number of draft emails differs from the expected.");
        }

        [Test]
        public void GetAddresseeSubjectBodyFromDraftEmail()
        {
            //Arrange
            homePage.GoToSignInForm();
            signInAndOutPage.LoginToEmail();
            homePage.ClickEmailButton();
            emailPage.SwitchToEmailPage();
            emailPage.CreateDraftEmail();

            string expectedAddressee = "Selenium WebDriver";
            string expectedSubject = "Greeting";
            string expectedBody = "Hi, Selenium!";

            //Act
            var actualAddressee = emailPage.GetActualAddressee();
            var actualSubject = emailPage.GetActualSubject();
            var actualBody = emailPage.GetActualBody();

            //Assert
            Assert.AreEqual(expectedAddressee, actualAddressee, "The actual addressee differs from the expected.");
            Assert.AreEqual(expectedSubject, actualSubject, "The actual subject differs from the expected.");
            Assert.AreEqual(expectedBody, actualBody, "The actual body differs from the expected.");
        }

        [Test]
        public void IsNotPresentInDraftSentEmail()
        {
            //Arrange
            homePage.GoToSignInForm();
            signInAndOutPage.LoginToEmail();
            homePage.ClickEmailButton();
            emailPage.SwitchToEmailPage();

            emailPage.CreateDraftEmail();

            var initialNumberOfDraftEmails = int.Parse(emailPage.GetNumberOfDraftEmails());

            emailPage.SendDraftEmail();
            emailPage.GoToDraftEmails();

            //Act
            int actualNumberOfDraftEmails;
            try
            {
                actualNumberOfDraftEmails = int.Parse(emailPage.GetNumberOfDraftEmails());
            }
            catch (Exception ex)
            {
                actualNumberOfDraftEmails = 0;
            }

            //Assert
            Assert.AreEqual(1, initialNumberOfDraftEmails - actualNumberOfDraftEmails, "The email is still in the draft folder.");
        }

        [Test]
        public void IsPresentInSentEmail()
        {
            //Arrange
            homePage.GoToSignInForm();
            signInAndOutPage.LoginToEmail();
            homePage.ClickEmailButton();
            emailPage.SwitchToEmailPage();

            emailPage.CreateDraftEmail();
            emailPage.SendDraftEmail();
            emailPage.GoToSentEmails();

            string expectedSubject = "Greeting";

            //Act
            var actualSubject = emailPage.GetActualSubject();

            //Assert
            Assert.AreEqual(expectedSubject, actualSubject, "The actual subject differs from the expected.");
        }

        [Test]
        public void GetLoggedOutUser()
        {
            //Arrange
            homePage.GoToSignInForm();
            signInAndOutPage.LoginToEmail();
            homePage.ClickEmailButton();
            emailPage.SwitchToEmailPage();

            emailPage.CreateDraftEmail();
            emailPage.SendDraftEmail();
            emailPage.GoToSentEmails();
            emailPage.GoToSignOutForm();

            string expectedLoginMessage = "Войдите с Яндекс ID, чтобы перейти к Почте";

            //Act
            var actualLoginMessage = signInAndOutPage.GetActualLoginMessage();

            //Assert
            Assert.AreEqual(expectedLoginMessage, actualLoginMessage, "Log Off failed.");
        }

        [Test]
        public void IsPresentInDeletedEmail()
        {
            //Arrange
            homePage.GoToSignInForm();
            signInAndOutPage.LoginToEmail();
            homePage.ClickEmailButton();
            emailPage.SwitchToEmailPage();

            emailPage.CreateDraftEmail();
            emailPage.DeleteDraftEmail();
            emailPage.GoToDeletedEmails();

            string expectedSubject = "Greeting";

            //Act
            var actualSubject = emailPage.GetActualSubject();

            //Assert
            Assert.AreEqual(expectedSubject, actualSubject, "The deleted email is not in the deleted folder.");
        }

        [Test]
        public void AreNotPresentInDraftDeletedEmails()
        {
            //Arrange
            homePage.GoToSignInForm();
            signInAndOutPage.LoginToEmail();
            homePage.ClickEmailButton();
            emailPage.SwitchToEmailPage();

            emailPage.CreateDraftEmail();
            emailPage.GoToDraftEmails();
            emailPage.DeleteAllDraftEmails();

            string expectedInfoMessage = "В папке «Черновики» нет писем.";

            //Act
            var actualInfoMessage = emailPage.GetActualInfoMessage();

            //Assert
            Assert.AreEqual(expectedInfoMessage, actualInfoMessage, "Emails are still in the draft folder.");
        }

        [Test]
        public void AreNotPresentInDeletedEmails()
        {
            //Arrange
            homePage.GoToSignInForm();
            signInAndOutPage.LoginToEmail();
            homePage.ClickEmailButton();
            emailPage.SwitchToEmailPage();

            emailPage.CreateDraftEmail();
            emailPage.DeleteDraftEmail();
            emailPage.GoToDeletedEmails();
            emailPage.ClickClearMark();

            string expectedInfoMessage = "В папке «Удалённые» нет писем.";

            //Act
            var actualInfoMessage = emailPage.GetActualInfoMessage();

            //Assert
            Assert.AreEqual(expectedInfoMessage, actualInfoMessage, "Emails are still in the draft folder.");
        }
    }
}