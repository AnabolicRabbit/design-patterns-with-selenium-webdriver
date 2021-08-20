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
            testName = TestContext.CurrentContext.Test.Name;
            test = extent.CreateTest(testName);
            this.Log.Info(string.Format("*{0}* test execution.", testName));

            //Act
            this.Log.Info("Getting the actual logged in user name from the top right of the mail box page.");
            var actualUserName = emailPage.GetActualUserName();

            //Assert
            this.Log.Info(string.Format("Comparing expected '{0}' and actual '{1}' user names.",
                ExpectedResults.expectedUserName, actualUserName));
            Assert.AreEqual(ExpectedResults.expectedUserName, actualUserName, "The actual logged in user differs from the expected.");
        }

        [Test]
        public void IsPresentInDraftEmail()
        {
            testName = TestContext.CurrentContext.Test.Name;
            test = extent.CreateTest(testName);
            this.Log.Info(string.Format("*{0}* test execution.", testName));

            //Act
            this.Log.Info(string.Format("Clicking 'Написать' button, populating 'Кому', 'Tема' with '{0}', the body with '{1}', " +
                "going to 'Черновики' folder.", subjectText, bodyText));
            emailPage.CreateDraftEmail(subjectText, bodyText);

            this.Log.Info("Getting the actual subject from the created draft email.");
            var actualSubject = emailPage.GetActualSubject();

            //Assert
            this.Log.Info(string.Format("Comparing expected '{0}' and actual '{1}' subjects.", subjectText, actualSubject));
            Assert.AreEqual(subjectText, actualSubject, "The actual subject differs from the expected.");
        }

        [Test]
        public void GetAddresseeSubjectBodyFromDraftEmail()
        {
            testName = TestContext.CurrentContext.Test.Name;
            test = extent.CreateTest(testName);
            this.Log.Info(string.Format("*{0}* test execution.", testName));

            //Act
            this.Log.Info(string.Format("Clicking 'Написать' button, populating 'Кому', 'Tема' with '{0}', the body with '{1}', " +
                "going to 'Черновики' folder.", subjectText, bodyText));
            emailPage.CreateDraftEmail(subjectText, bodyText);

            this.Log.Info("Getting the actual addressee, subject, body from the created draft email.");
            var actualAddressee = emailPage.GetActualAddressee();
            var actualSubject = emailPage.GetActualSubject();
            var actualBody = emailPage.GetActualBody();

            //Assert
            this.Log.Info(string.Format("Comparing expected '{0}' and actual '{1}' addressees.",
                ExpectedResults.expectedAddressee, actualAddressee));
            Assert.AreEqual(ExpectedResults.expectedAddressee, actualAddressee, "The actual addressee differs from the expected.");
            this.Log.Info(string.Format("Comparing expected '{0}' and actual '{1}' subjects.", subjectText, actualSubject));
            Assert.AreEqual(subjectText, actualSubject, "The actual subject differs from the expected.");
            this.Log.Info(string.Format("Comparing expected '{0}' and actual '{1}' bodies.", bodyText, actualBody));
            Assert.AreEqual(bodyText, actualBody, "The actual body differs from the expected.");
        }

        [Test]
        public void IsNotPresentInDraftSentEmail()
        {
            testName = TestContext.CurrentContext.Test.Name;
            test = extent.CreateTest(testName);
            this.Log.Info(string.Format("*{0}* test execution.", testName));

            //Act
            this.Log.Info(string.Format("Clicking 'Написать' button, populating 'Кому', 'Tема' with '{0}', the body with '{1}', " +
                "going to 'Черновики' folder.", subjectText, bodyText));
            emailPage.CreateDraftEmail(subjectText, bodyText);

            this.Log.Info("Getting the number of draft emails before sending.");
            var numberOfDraftEmailsBeforeSending = emailPage.GetNumberOfDraftEmails();

            this.Log.Info("Sending the draft email by clicking 'Отправить' button.");
            emailPage.SendDraftEmail();
            this.Log.Info("Going to 'Черновики' folder.");
            emailPage.GoToDraftEmails();

            this.Log.Info("Getting the number of draft emails after sending.");
            var numberOfDraftEmailsAfterSending = emailPage.GetNumberOfDraftEmails();

            this.Log.Info("Getting actual draft emails difference before and after sending.");
            var actualDraftEmailsDifference = numberOfDraftEmailsBeforeSending - numberOfDraftEmailsAfterSending;

            //Assert
            this.Log.Info(string.Format("Comparing expected '{0}' and actual '{1}' draft emails difference.",
                ExpectedResults.expectedDraftEmailsDifference, actualDraftEmailsDifference));
            Assert.AreEqual(ExpectedResults.expectedDraftEmailsDifference, actualDraftEmailsDifference, "The email is still in the draft folder.");
        }

        [Test]
        public void IsPresentInSentEmail()
        {
            testName = TestContext.CurrentContext.Test.Name;
            test = extent.CreateTest(testName);
            this.Log.Info(string.Format("*{0}* test execution.", testName));

            //Act
            this.Log.Info(string.Format("Clicking 'Написать' button, populating 'Кому', 'Tема' with '{0}', the body with '{1}', " +
                "going to 'Черновики' folder.", subjectText, bodyText));
            emailPage.CreateDraftEmail(subjectText, bodyText);
            this.Log.Info("Sending the draft email by clicking 'Отправить' button.");
            emailPage.SendDraftEmail();
            this.Log.Info("Going to 'Отправленные' folder.");
            emailPage.GoToSentEmails();

            this.Log.Info("Getting the actual subject from the sent email.");
            var actualSubject = emailPage.GetActualSubject();

            //Assert
            this.Log.Info(string.Format("Comparing expected '{0}' and actual '{1}' subjects.", subjectText, actualSubject));
            Assert.AreEqual(subjectText, actualSubject, "The actual subject differs from the expected.");
        }

        [Test]
        public void GetLoggedOutUser()
        {
            testName = TestContext.CurrentContext.Test.Name;
            test = extent.CreateTest(testName);
            this.Log.Info(string.Format("*{0}* test execution.", testName));

            //Act
            this.Log.Info(string.Format("Clicking 'Написать' button, populating 'Кому', 'Tема' with '{0}', the body with '{1}', " +
                "going to 'Черновики' folder.", subjectText, bodyText));
            emailPage.CreateDraftEmail(subjectText, bodyText);
            this.Log.Info("Sending the draft email by clicking 'Отправить' button.");
            emailPage.SendDraftEmail();
            this.Log.Info("Going to 'Отправленные' folder.");
            emailPage.GoToSentEmails();
            this.Log.Info("Going to the sign out form by clicking the user logo and 'Выйти из сервисов Яндекса' button.");
            emailPage.GoToSignOutForm();

            this.Log.Info("Getting the actual login message from the sign out page.");
            var actualLoginMessage = signInAndOutPage.GetActualLoginMessage();

            //Assert
            this.Log.Info(string.Format("Comparing expected '{0}' and actual '{1}' login messages.",
                ExpectedResults.expectedLoginMessage, actualLoginMessage));
            Assert.AreEqual(ExpectedResults.expectedLoginMessage, actualLoginMessage, "Log Off failed.");
        }

        [Test]
        public void IsPresentInDeletedEmail()
        {
            testName = TestContext.CurrentContext.Test.Name;
            test = extent.CreateTest(testName);
            this.Log.Info(string.Format("*{0}* test execution.", testName));

            //Act
            this.Log.Info(string.Format("Clicking 'Написать' button, populating 'Кому', 'Tема' with '{0}', the body with '{1}', " +
                "going to 'Черновики' folder.", subjectText, bodyText));
            emailPage.CreateDraftEmail(subjectText, bodyText);
            this.Log.Info("Deleting the draft email by checking it and clicking 'Удалить' button.");
            emailPage.DeleteDraftEmail();
            this.Log.Info("Going to 'Удалённые' folder.");
            emailPage.GoToDeletedEmails();

            this.Log.Info("Getting the actual subject from the deleted email.");
            var actualSubject = emailPage.GetActualSubject();

            //Assert
            this.Log.Info(string.Format("Comparing expected '{0}' and actual '{1}' subjects.", subjectText, actualSubject));
            Assert.AreEqual(subjectText, actualSubject, "The deleted email is not in the deleted folder.");
        }

        [Test]
        public void AreNotPresentInDraftDeletedEmails()
        {
            testName = TestContext.CurrentContext.Test.Name;
            test = extent.CreateTest(testName);
            this.Log.Info(string.Format("*{0}* test execution.", testName));

            //Act
            this.Log.Info(string.Format("Clicking 'Написать' button, populating 'Кому', 'Tема' with '{0}', the body with '{1}', " +
                "going to 'Черновики' folder.", subjectText, bodyText));
            emailPage.CreateDraftEmail(subjectText, bodyText);
            this.Log.Info("Going to 'Черновики' folder.");
            emailPage.GoToDraftEmails();
            this.Log.Info("Deleting all draft emails by checking them and clicking 'Удалить' button.");
            emailPage.DeleteAllDraftEmails();

            this.Log.Info("Getting the actual info message.");
            var actualInfoMessage = emailPage.GetActualInfoMessage();

            //Assert
            this.Log.Info(string.Format("Comparing expected '{0}' and actual '{1}' info messages.",
                ExpectedResults.expectedDraftInfoMessage, actualInfoMessage));
            Assert.AreEqual(ExpectedResults.expectedDraftInfoMessage, actualInfoMessage, "Emails are still in the draft folder.");
        }

        [Test]
        public void AreNotPresentInDeletedEmails()
        {
            testName = TestContext.CurrentContext.Test.Name;
            test = extent.CreateTest(testName);
            this.Log.Info(string.Format("*{0}* test execution.", testName));

            //Act
            this.Log.Info(string.Format("Clicking 'Написать' button, populating 'Кому', 'Tема' with '{0}', the body with '{1}', " +
                "going to 'Черновики' folder.", subjectText, bodyText));
            emailPage.CreateDraftEmail(subjectText, bodyText);
            this.Log.Info("Deleting the draft email by checking it and clicking 'Удалить' button.");
            emailPage.DeleteDraftEmail();
            this.Log.Info("Going to 'Удалённые' folder.");
            emailPage.GoToDeletedEmails();
            this.Log.Info("Clearing the 'Удалённые' folder by clicking 'Очистить' button.");
            emailPage.ClickClearMark();

            this.Log.Info("Getting the actual info message.");
            var actualInfoMessage = emailPage.GetActualInfoMessage();

            //Assert
            this.Log.Info(string.Format("Comparing expected '{0}' and actual '{1}' info messages.",
                ExpectedResults.expectedDeletedInfoMessage, actualInfoMessage));
            Assert.AreEqual(ExpectedResults.expectedDeletedInfoMessage, actualInfoMessage, "Emails are still in the deleted folder.");
        }

        [Test]
        public void DeletedByDraggingPictureIsPresentInRecyclerBin()
        {
            testName = TestContext.CurrentContext.Test.Name;
            test = extent.CreateTest(testName);
            this.Log.Info(string.Format("*{0}* test execution.", testName));

            //Act
            this.Log.Info("Going to the yandex disk by clicking '' icon.");
            emailPage.GoToDisk();

            var expectedDeletedPictureName = diskPage.GetPictureName(pictureItemName);

            this.Log.Info("Moving the picture to the recycler bin by dragging and dropping it.");
            diskPage.MovePictureToRecyclerBin(pictureItemName, recycleBinItemName);
            this.Log.Info("Going to the recycler bin by clicking 'Корзина' icon.");
            diskPage.GoToRecyclerBin(recycleBinItemName);

            this.Log.Info("Getting the actual deleted picture name.");
            var actualDeletedPictureName = diskPage.GetPictureName(pictureItemName);

            //Assert
            this.Log.Info(string.Format("Comparing expected '{0}' and actual '{1}' picture names.",
                expectedDeletedPictureName, actualDeletedPictureName));
            Assert.AreEqual(expectedDeletedPictureName, actualDeletedPictureName, "Picture is not in the recycle bin.");
        }
    }
}