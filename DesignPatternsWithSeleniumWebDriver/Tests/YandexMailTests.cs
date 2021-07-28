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

        [Test]
        public void GetLoggedInUser()
        {
            //Act
            var actualUserName = emailPage.GetActualUserName();

            //Assert
            Assert.AreEqual(ExpectedResults.expectedUserName, actualUserName, "The actual logged in user differs from the expected.");
        }

        [Test]
        public void IsPresentInDraftEmail()
        {
            //Act
            emailPage.CreateDraftEmail(subjectText, bodyText);

            var actualSubject = emailPage.GetActualSubject();

            //Assert
            Assert.AreEqual(subjectText, actualSubject, "The actual subject differs from the expected.");
        }

        [Test]
        public void GetAddresseeSubjectBodyFromDraftEmail()
        {
            //Act
            emailPage.CreateDraftEmail(subjectText, bodyText);

            var actualAddressee = emailPage.GetActualAddressee();
            var actualSubject = emailPage.GetActualSubject();
            var actualBody = emailPage.GetActualBody();

            //Assert
            Assert.AreEqual(ExpectedResults.expectedAddressee, actualAddressee, "The actual addressee differs from the expected.");
            Assert.AreEqual(subjectText, actualSubject, "The actual subject differs from the expected.");
            Assert.AreEqual(bodyText, actualBody, "The actual body differs from the expected.");
        }

        [Test]
        public void IsNotPresentInDraftSentEmail()
        {
            //Act
            emailPage.CreateDraftEmail(subjectText, bodyText);

            var numberOfDraftEmailsBeforeSending = emailPage.GetNumberOfDraftEmails();

            emailPage.SendDraftEmail();
            emailPage.GoToDraftEmails();

            var numberOfDraftEmailsAfterSending = emailPage.GetNumberOfDraftEmails();

            var actualDraftEmailsDifference = numberOfDraftEmailsBeforeSending - numberOfDraftEmailsAfterSending;

            //Assert
            Assert.AreEqual(ExpectedResults.expectedDraftEmailsDifference, actualDraftEmailsDifference, "The email is still in the draft folder.");
        }

        [Test]
        public void IsPresentInSentEmail()
        {
            //Act
            emailPage.CreateDraftEmail(subjectText, bodyText);
            emailPage.SendDraftEmail();
            emailPage.GoToSentEmails();

            var actualSubject = emailPage.GetActualSubject();

            //Assert
            Assert.AreEqual(subjectText, actualSubject, "The actual subject differs from the expected.");
        }

        [Test]
        public void GetLoggedOutUser()
        {
            //Act
            emailPage.CreateDraftEmail(subjectText, bodyText);
            emailPage.SendDraftEmail();
            emailPage.GoToSentEmails();
            emailPage.GoToSignOutForm();

            var actualLoginMessage = signInAndOutPage.GetActualLoginMessage();

            //Assert
            Assert.AreEqual(ExpectedResults.expectedLoginMessage, actualLoginMessage, "Log Off failed.");
        }

        [Test]
        public void IsPresentInDeletedEmail()
        {
            //Act
            emailPage.CreateDraftEmail(subjectText, bodyText);
            emailPage.DeleteDraftEmail();
            emailPage.GoToDeletedEmails();

            var actualSubject = emailPage.GetActualSubject();

            //Assert
            Assert.AreEqual(subjectText, actualSubject, "The deleted email is not in the deleted folder.");
        }

        [Test]
        public void AreNotPresentInDraftDeletedEmails()
        {
            //Act
            emailPage.CreateDraftEmail(subjectText, bodyText);
            emailPage.GoToDraftEmails();
            emailPage.DeleteAllDraftEmails();

            var actualInfoMessage = emailPage.GetActualInfoMessage();

            //Assert
            Assert.AreEqual(ExpectedResults.expectedDraftInfoMessage, actualInfoMessage, "Emails are still in the draft folder.");
        }

        [Test]
        public void AreNotPresentInDeletedEmails()
        {
            //Act
            emailPage.CreateDraftEmail(subjectText, bodyText);
            emailPage.DeleteDraftEmail();
            emailPage.GoToDeletedEmails();
            emailPage.ClickClearMark();

            var actualInfoMessage = emailPage.GetActualInfoMessage();

            //Assert
            Assert.AreEqual(ExpectedResults.expectedDeletedInfoMessage, actualInfoMessage, "Emails are still in the deleted folder.");
        }

        [Test]
        public void DeletedByDraggingPictureIsPresentInRecyclerBin()
        {
            //Act
            emailPage.GoToDisk();

            var expectedDeletedPictureName = diskPage.GetPictureName(pictureItemName);

            diskPage.MovePictureToRecyclerBin(pictureItemName, recycleBinItemName);
            diskPage.GoToRecyclerBin(recycleBinItemName);

            var actualDeletedPictureName = diskPage.GetPictureName(pictureItemName);

            //Assert
            Assert.AreEqual(expectedDeletedPictureName, actualDeletedPictureName, "Picture is not in the recycle bin.");
        }
    }
}