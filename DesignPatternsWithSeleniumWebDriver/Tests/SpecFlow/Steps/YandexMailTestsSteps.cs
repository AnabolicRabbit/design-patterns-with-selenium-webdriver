using DesignPatternsWithSeleniumWebDriver.Entity;
using DesignPatternsWithSeleniumWebDriver.Utilities;
using DesignPatternsWithSeleniumWebDriver.WebDriver;
using DesignPatternsWithSeleniumWebDriver.WebObjects;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace DesignPatternsWithSeleniumWebDriver.Tests.SpecFlow.Steps
{
    [Binding]
    public sealed class YandexMailTestsSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HomePage homePage = new HomePage();
        private readonly SignInAndOutPage signInAndOutPage = new SignInAndOutPage();
        private readonly EmailPage emailPage = new EmailPage();
        private readonly DiskPage diskPage = new DiskPage();

        public YandexMailTestsSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I log in to yandex mail box")]
        public void InitializeTest()
        {
            Browser.GetInstance();
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

        private string actualUserName;

        [When(@"I get the actual user name from User Account option")]
        public string GetActualUserName()
        {
            actualUserName = emailPage.GetActualUserName();
            return actualUserName;
        }

        [Then(@"the actual user name should match the entered login")]
        public void CompareActualAndExpectedLoggedInUserName()
        {
            Assert.AreEqual(ExpectedResults.expectedUserName, actualUserName, "The actual logged in user differs from the expected.");
        }

        [When(@"I create a draft email with '(.*)' and '(.*)'")]
        public void CreateDraftEmail(string subjectText, string bodyText)
        {
            emailPage.CreateDraftEmail(subjectText, bodyText);
        }

        private string actualSubject;

        [When(@"get the actual subject")]
        public string GetActualCreatedDraftEmailSubject()
        {
            actualSubject = emailPage.GetActualSubject();
            return actualSubject;
        }

        [Then(@"the email with '(.*)' should be present in the folder")]
        public void CompareExpectedAndActualCreatedDraftEmailSubject(string subjectText)
        {
            Assert.AreEqual(subjectText, actualSubject, "The actual subject differs from the expected.");
        }

        private string actualAddressee;

        [When(@"get the actual addressee")]
        public string GetActualCreatedDraftEmailAddressee()
        {
            actualAddressee = emailPage.GetActualAddressee();
            return actualAddressee;
        }

        private string actualBody;

        [When(@"get the actual body")]
        public string GetActualCreatedDraftEmailBody()
        {
            actualBody = emailPage.GetActualBody();
            return actualBody;
        }

        [Then(@"the expected addresse, the '(.*)' and the '(.*)' from the draft email should match the entered ones")]
        public void CompareExpectedAndActualCreatedDraftEmailAddresseSubjectBody(string subjectText, string bodyText)
        {
            Assert.AreEqual(ExpectedResults.expectedAddressee, actualAddressee, "The actual addressee differs from the expected.");
            Assert.AreEqual(subjectText, actualSubject, "The actual subject differs from the expected.");
            Assert.AreEqual(bodyText, actualBody, "The actual body differs from the expected.");
        }

        private int numberOfDraftEmailsBeforeSending;

        [When(@"get number of emails in the Draft folder before sending")]
        public int GetNumberOfDraftEmailsBeforeSending()
        {
            numberOfDraftEmailsBeforeSending = emailPage.GetNumberOfDraftEmails();
            return numberOfDraftEmailsBeforeSending;
        }

        [When(@"send the created draft email")]
        public void SendDraftEmail()
        {
            emailPage.SendDraftEmail();
        }

        [When(@"navigate to the Draft folder")]
        public void NavigateToDraftFolder()
        {
            emailPage.GoToDraftEmails();
        }

        private int actualDraftEmailsDifference;

        [When(@"get difference between emails number before and after sending")]
        public int GetActualDifferenceBetweenEmailsNumberBeforeAndAfterSending()
        {
            var numberOfDraftEmailsAfterSending = emailPage.GetNumberOfDraftEmails();

            actualDraftEmailsDifference = numberOfDraftEmailsBeforeSending - numberOfDraftEmailsAfterSending;
            return actualDraftEmailsDifference;
        }

        [Then(@"the sent email should not be present in Draft folder")]
        public void CompareExpectedAndActualDraftEmailsDifference()
        {
            Assert.AreEqual(ExpectedResults.expectedDraftEmailsDifference, actualDraftEmailsDifference, "The email is still in the draft folder.");
        }

        [When(@"navigate to the Sent folder")]
        public void NavigateToSentFolder()
        {
            emailPage.GoToSentEmails();
        }

        [When(@"navigate to the Sign Out form")]
        public void NavigateToSignOutForm()
        {
            emailPage.GoToSignOutForm();
        }

        private string actualLoginMessage;

        [When(@"get the actual login message")]
        public string GetActualLoginMessage()
        {
            actualLoginMessage = signInAndOutPage.GetActualLoginMessage();
            return actualLoginMessage;
        }

        [Then(@"the expected login message should be displayed on the form")]
        public void CompareExpectedAndActualLoginMessage()
        {
            Assert.AreEqual(ExpectedResults.expectedLoginMessage, actualLoginMessage, "Log Off failed.");
        }

        [When(@"delete the draft email")]
        public void DeleteDraftEmail()
        {
            emailPage.DeleteDraftEmail();
        }

        [When(@"navigate to the Deleted folder")]
        public void NavigateToDeletedFolder()
        {
            emailPage.GoToDeletedEmails();
        }

        [When(@"delete all draft emails")]
        public void DeleteAllDraftEmails()
        {
            emailPage.DeleteAllDraftEmails();
        }

        private string actualInfoMessage;

        [When(@"get the actual info message")]
        public string GetActualInfoMessage()
        {
            actualInfoMessage = emailPage.GetActualInfoMessage();
            return actualInfoMessage;
        }

        [Then(@"the expected info message should be displayed in the Draft folder")]
        public void CompareExpectedAndActualInfoMessageInDraftFolder()
        {
            Assert.AreEqual(ExpectedResults.expectedDraftInfoMessage, actualInfoMessage, "Emails are still in the draft folder.");
        }

        [When(@"click the clear mark")]
        public void ClickClearMark()
        {
            emailPage.ClickClearMark();
        }

        [Then(@"the expected info message should be displayed in the Deleted folder")]
        public void CompareExpectedAndActualInfoMessageInDeletedFolder()
        {
            Assert.AreEqual(ExpectedResults.expectedDeletedInfoMessage, actualInfoMessage, "Emails are still in the deleted folder.");
        }

        [When(@"I navigate to the Disk folder")]
        public void NavigateToDiskFolder()
        {
            emailPage.GoToDisk();
        }

        private string expectedDeletedPictureName;

        [When(@"get a picture for deletion '(.*)'")]
        public string GetPictureForDeletionName(string pictureItemName)
        {
            expectedDeletedPictureName = diskPage.GetPictureName(pictureItemName);
            return expectedDeletedPictureName;
        }

        [When(@"drag the pictire with '(.*)' to the RecyclerBin '(.*)'")]
        public void DragPictireToRecyclerBin(string pictureItemName, string recycleBinItemName)
        {
            diskPage.MovePictureToRecyclerBin(pictureItemName, recycleBinItemName);
        }

        [When(@"navigate to the Recycler Bin '(.*)'")]
        public void NavigateToRecycleBin(string recycleBinItemName)
        {
            diskPage.GoToRecyclerBin(recycleBinItemName);
        }

        private string actualDeletedPictureName;

        [When(@"get the deleted picture '(.*)'")]
        public string GetDeletedPictureName(string pictureItemName)
        {
            actualDeletedPictureName = diskPage.GetPictureName(pictureItemName);
            return actualDeletedPictureName;
        }

        [Then(@"the deleted picture should be present in the Recycle Bin")]
        public void CompareExpectedAndActualDeletedPictureName()
        {
            Assert.AreEqual(expectedDeletedPictureName, actualDeletedPictureName, "Picture is not in the recycle bin.");
        }

        [AfterScenario]
        public static void CleanTest()
        {
            Browser.Driver.Close();
            Browser.Driver.Quit();
            Browser.Quit();
        }
    }
}