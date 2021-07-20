using DesignPatternsWithSeleniumWebDriver.Utilities;
using DesignPatternsWithSeleniumWebDriver.WebObjects;
using NUnit.Framework;

namespace DesignPatternsWithSeleniumWebDriver
{
    [TestFixture]
    public class YandexMailTests : BaseTest
    {
        private readonly SignInAndOutPage signInAndOutPage = new SignInAndOutPage();
        private readonly EmailPage emailPage = new EmailPage();
        private readonly DiskPage diskPage = new DiskPage();
        private readonly ExpectedResults expectedResults = new ExpectedResults();

        [Test]
        public void GetLoggedInUser()
        {
            //Act
            var actualUserName = emailPage.GetActualUserName();

            //Assert
            Assert.AreEqual(expectedResults.expectedUserName, actualUserName, "The actual logged in user differs from the expected.");
        }

        [Test]
        public void IsPresentInDraftEmail()
        {
            //Act
            emailPage.CreateDraftEmail();

            var actualSubject = emailPage.GetActualSubject();

            //Assert
            Assert.AreEqual(expectedResults.expectedSubject, actualSubject, "The actual subject differs from the expected.");
        }

        [Test]
        public void GetAddresseeSubjectBodyFromDraftEmail()
        {
            //Act
            emailPage.CreateDraftEmail();

            var actualAddressee = emailPage.GetActualAddressee();
            var actualSubject = emailPage.GetActualSubject();
            var actualBody = emailPage.GetActualBody();

            //Assert
            Assert.AreEqual(expectedResults.expectedAddressee, actualAddressee, "The actual addressee differs from the expected.");
            Assert.AreEqual(expectedResults.expectedSubject, actualSubject, "The actual subject differs from the expected.");
            Assert.AreEqual(expectedResults.expectedBody, actualBody, "The actual body differs from the expected.");
        }

        [Test]
        public void IsNotPresentInDraftSentEmail()
        {
            //Act
            emailPage.CreateDraftEmail();

            emailPage.GetInitialNumberOfDraftEmails();

            emailPage.SendDraftEmail();
            emailPage.GoToDraftEmails();

            emailPage.GetActualNumberOfDraftEmails();

            var actualDraftEmailsDifference = emailPage.initialNumberOfDraftEmails - emailPage.actualNumberOfDraftEmails;

            //Assert
            Assert.AreEqual(expectedResults.expectedDraftEmailsDifference, actualDraftEmailsDifference, "The email is still in the draft folder.");
        }

        [Test]
        public void IsPresentInSentEmail()
        {
            //Act
            emailPage.CreateDraftEmail();
            emailPage.SendDraftEmail();
            emailPage.GoToSentEmails();

            var actualSubject = emailPage.GetActualSubject();

            //Assert
            Assert.AreEqual(expectedResults.expectedSubject, actualSubject, "The actual subject differs from the expected.");
        }

        [Test]
        public void GetLoggedOutUser()
        {
            //Act
            emailPage.CreateDraftEmail();
            emailPage.SendDraftEmail();
            emailPage.GoToSentEmails();
            emailPage.GoToSignOutForm();

            var actualLoginMessage = signInAndOutPage.GetActualLoginMessage();

            //Assert
            Assert.AreEqual(expectedResults.expectedLoginMessage, actualLoginMessage, "Log Off failed.");
        }

        [Test]
        public void IsPresentInDeletedEmail()
        {
            //Act
            emailPage.CreateDraftEmail();
            emailPage.DeleteDraftEmail();
            emailPage.GoToDeletedEmails();

            var actualSubject = emailPage.GetActualSubject();

            //Assert
            Assert.AreEqual(expectedResults.expectedSubject, actualSubject, "The deleted email is not in the deleted folder.");
        }

        [Test]
        public void AreNotPresentInDraftDeletedEmails()
        {
            //Act
            emailPage.CreateDraftEmail();
            emailPage.GoToDraftEmails();
            emailPage.DeleteAllDraftEmails();

            var actualInfoMessage = emailPage.GetActualInfoMessage();

            //Assert
            Assert.AreEqual(expectedResults.expectedDraftInfoMessage, actualInfoMessage, "Emails are still in the draft folder.");
        }

        [Test]
        public void AreNotPresentInDeletedEmails()
        {
            //Act
            emailPage.CreateDraftEmail();
            emailPage.DeleteDraftEmail();
            emailPage.GoToDeletedEmails();
            emailPage.ClickClearMark();

            var actualInfoMessage = emailPage.GetActualInfoMessage();

            //Assert
            Assert.AreEqual(expectedResults.expectedDeletedInfoMessage, actualInfoMessage, "Emails are still in the deleted folder.");
        }

        [Test]
        public void DeletedByDraggingPictureIsPresentInRecyclerBin()
        {
            //Act
            emailPage.GoToDisk();

            expectedResults.GetExpectedPictureName();

            diskPage.MovePictureToRecyclerBin();
            diskPage.GoToRecyclerBin();

            var actualDeletedPictureName = diskPage.GetDeletedPictureName();

            //Assert
            Assert.AreEqual(expectedResults.expectedDeletedPictureName, actualDeletedPictureName, "Picture is not in the recycle bin.");
        }
    }
}