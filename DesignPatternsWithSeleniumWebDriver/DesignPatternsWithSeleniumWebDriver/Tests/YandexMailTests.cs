using DesignPatternsWithSeleniumWebDriver.WebObjects;
using NUnit.Framework;
using OpenQA.Selenium;

namespace DesignPatternsWithSeleniumWebDriver
{
    [TestFixture]
    public class YandexMailTests : BaseTest
    {
        private readonly SignInAndOutPage signInAndOutPage = new SignInAndOutPage();
        private readonly EmailPage emailPage = new EmailPage();
        private readonly DiskPage diskPage = new DiskPage();

        [Test]
        public void GetLoggedInUser()
        {
            //Arrange
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
            string expectedSubject = "Greeting";

            //Act
            emailPage.CreateDraftEmail();

            var actualSubject = emailPage.GetActualSubject();

            //Assert
            Assert.AreEqual(expectedSubject, actualSubject, "The actual subject differs from the expected.");
        }

        [Test]
        public void GetAddresseeSubjectBodyFromDraftEmail()
        {
            //Arrange
            string expectedAddressee = "Selenium WebDriver";
            string expectedSubject = "Greeting";
            string expectedBody = "Hi, Selenium!";

            //Act
            emailPage.CreateDraftEmail();

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
            //Act
            emailPage.CreateDraftEmail();

            var initialNumberOfDraftEmails = int.Parse(emailPage.GetNumberOfDraftEmails());

            emailPage.SendDraftEmail();
            emailPage.GoToDraftEmails();

            int actualNumberOfDraftEmails;
            try
            {
                actualNumberOfDraftEmails = int.Parse(emailPage.GetNumberOfDraftEmails());
            }
            catch (NoSuchElementException)
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
            string expectedSubject = "Greeting";

            //Act
            emailPage.CreateDraftEmail();
            emailPage.SendDraftEmail();
            emailPage.GoToSentEmails();

            var actualSubject = emailPage.GetActualSubject();

            //Assert
            Assert.AreEqual(expectedSubject, actualSubject, "The actual subject differs from the expected.");
        }

        [Test]
        public void GetLoggedOutUser()
        {
            //Arrange
            string expectedLoginMessage = "Войдите с Яндекс ID, чтобы перейти к Почте";

            //Act
            emailPage.CreateDraftEmail();
            emailPage.SendDraftEmail();
            emailPage.GoToSentEmails();
            emailPage.GoToSignOutForm();

            var actualLoginMessage = signInAndOutPage.GetActualLoginMessage();

            //Assert
            Assert.AreEqual(expectedLoginMessage, actualLoginMessage, "Log Off failed.");
        }

        [Test]
        public void IsPresentInDeletedEmail()
        {
            //Arrange
            string expectedSubject = "Greeting";

            //Act
            emailPage.CreateDraftEmail();
            emailPage.DeleteDraftEmail();
            emailPage.GoToDeletedEmails();

            var actualSubject = emailPage.GetActualSubject();

            //Assert
            Assert.AreEqual(expectedSubject, actualSubject, "The deleted email is not in the deleted folder.");
        }

        [Test]
        public void AreNotPresentInDraftDeletedEmails()
        {
            //Arrange
            string expectedInfoMessage = "В папке «Черновики» нет писем.";

            //Act
            emailPage.CreateDraftEmail();
            emailPage.GoToDraftEmails();
            emailPage.DeleteAllDraftEmails();

            var actualInfoMessage = emailPage.GetActualInfoMessage();

            //Assert
            Assert.AreEqual(expectedInfoMessage, actualInfoMessage, "Emails are still in the draft folder.");
        }

        [Test]
        public void AreNotPresentInDeletedEmails()
        {
            //Arrange
            string expectedInfoMessage = "В папке «Удалённые» нет писем.";

            //Act
            emailPage.CreateDraftEmail();
            emailPage.DeleteDraftEmail();
            emailPage.GoToDeletedEmails();
            emailPage.ClickClearMark();

            var actualInfoMessage = emailPage.GetActualInfoMessage();

            //Assert
            Assert.AreEqual(expectedInfoMessage, actualInfoMessage, "Emails are still in the draft folder.");
        }

        [Test]
        public void DeletedByDraggingPictureIsPresentInRecyclerBin()
        {
            //Act
            emailPage.GoToDisk();

            var expectedDeletedPictureName = diskPage.GetPictureName();

            diskPage.MovePictureToRecyclerBin();
            diskPage.GoToRecyclerBin();
            
            var actualDeletedPictureName = diskPage.GetDeletedPictureName();

            //Assert
            Assert.AreEqual(expectedDeletedPictureName, actualDeletedPictureName, "Picture is not in the recycle bin.");
        }
    }
}