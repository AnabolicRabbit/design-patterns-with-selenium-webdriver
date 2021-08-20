using OpenQA.Selenium;
using System.Linq;
using DesignPatternsWithSeleniumWebDriver.WebDriver;
using DesignPatternsWithSeleniumWebDriver.Logging;
using System;

namespace DesignPatternsWithSeleniumWebDriver.WebObjects
{
    public class EmailPage : BasePage
    {
        private static readonly By loggedInUser = By.ClassName("user-account__name");

        public EmailPage() : base(loggedInUser, "Email Page") { }

        private readonly BaseElement writeEmailButton = new BaseElement(By.XPath("//a[@class='mail-ComposeButton js-main-action-compose']"));
        private readonly BaseElement toWhomField = new BaseElement(By.ClassName("composeYabbles"));
        private readonly BaseElement toWhomOption = new BaseElement(By.ClassName("ContactsSuggestItemDesktop-Email"));
        private readonly BaseElement subjectField = new BaseElement(By.XPath("//input[@class='composeTextField ComposeSubject-TextField']"));
        private readonly BaseElement bodyField = new BaseElement(By.XPath("//div[contains(@class, 'cke_enable_context_menu')]"));
        private readonly BaseElement draftsOption = new BaseElement(By.XPath("//a[@data-title='Черновики']"));
        private readonly BaseElement draftEmail = new BaseElement(By.XPath("//span[@class='mail-MessageSnippet-FromText']"));
        private readonly BaseElement sendButton = new BaseElement(By.XPath("(//button[contains(@class, 'ComposeControlPanelButton-Button')])[1]"));
        private readonly BaseElement popupWindow = new BaseElement(By.XPath("//a[@class='control link link_theme_normal ComposeDoneScreen-Link']"));
        private readonly BaseElement sentOption = new BaseElement(By.XPath("//a[@data-title='Отправленные']"));
        private readonly BaseElement userLogo = new BaseElement(By.XPath("(//div/img[@class='user-pic__image'])[1]"));
        private readonly BaseElement signOutOption = new BaseElement(By.XPath("//span[text()='Выйти из сервисов Яндекса']"));
        private readonly BaseElement emailCheckbox = new BaseElement(By.XPath("(//div[contains(@class, 'js-messages-item-checkbox')]/label)[1]"));
        private readonly BaseElement deleteButton = new BaseElement(By.XPath("//span[text()='Удалить']"));
        private readonly BaseElement deletedOption = new BaseElement(By.XPath("//a[@data-title='Удалённые']"));
        private readonly BaseElement allEmailsCheckbox = new BaseElement(By.XPath("(//span[@class='checkbox_view'])[1]"));
        private readonly BaseElement clearMark = new BaseElement(By.XPath("//span[@class='mail-NestedList-Item-Clean ns-action']"));
        private readonly BaseElement clearButton = new BaseElement(By.XPath("//span[text()='Очистить']"));
        private readonly BaseElement clearOption = new BaseElement(By.XPath("//span[@class='_nb-button-text' and text()='Очистить']"));
        private readonly BaseElement diskIcon = new BaseElement(By.XPath("//div[contains(@class, 'PSHeaderIcon-Image_Disk')]"));
        private readonly BaseElement actualUserName = new BaseElement(By.XPath("//a[contains(@class, 'user-account_left-name')]/span[1]"));
        private readonly BaseElement actualAddressee = new BaseElement(By.XPath("(//span[contains(@class, 'mail-MessageSnippet-Item_sender')]/span)[1]"));
        private readonly BaseElement actualSubject = new BaseElement(By.XPath("(//span[contains(@class, 'mail-MessageSnippet-Item_subject')]/span)[1]"));
        private readonly BaseElement actualBody = new BaseElement(By.XPath("(//span[contains(@class, 'mail-MessageSnippet-Item_firstline')]/span)[1]"));
        private readonly BaseElement numberOfDraftEmailsMark = new BaseElement(By.XPath("//a[@href='#draft']/div/span"));
        private readonly BaseElement actualInfoMessage = new BaseElement(By.XPath("(//div[@class='b-messages__placeholder']/div)[1]"));

        public Logger Log;

        public void SwitchToEmailPage()
        {
            var currentTab = Browser.Driver.WindowHandles.Last();
            Browser.Driver.SwitchTo().Window(currentTab);
        }

        public void CreateDraftEmail(string subjectText, string bodyText)
        {
            writeEmailButton.Click();
            toWhomField.Click();
            toWhomOption.Click();
            subjectField.SendKeys(subjectText);
            bodyField.SendKeys(bodyText);
            draftsOption.Click();
        }

        public void SendDraftEmail()
        {
            draftEmail.Click();
            sendButton.Click();
            popupWindow.Click();
        }

        public void GoToDraftEmails()
        {
            draftsOption.Click();
        }

        public void GoToSentEmails()
        {
            sentOption.Click();
        }

        public void GoToSignOutForm()
        {
            userLogo.Click();
            signOutOption.Click();
        }

        public void DeleteDraftEmail()
        {
            emailCheckbox.Click();
            deleteButton.Click();
        }

        public void GoToDeletedEmails()
        {
            deletedOption.Click();
        }

        public void DeleteAllDraftEmails()
        {
            allEmailsCheckbox.Click();
            deleteButton.Click();
        }

        public void ClickClearMark()
        {
            clearMark.Click();
            clearButton.Click();
            clearOption.Click();
        }

        public string GetActualUserName()
        {
            return actualUserName.GetText();
        }

        public string GetActualAddressee()
        {
            return actualAddressee.GetText();
        }

        public string GetActualSubject()
        {
            return actualSubject.GetText();
        }

        public string GetActualBody()
        {
            return actualBody.GetText();
        }

        public int GetNumberOfDraftEmails()
        {
            int numberOfDraftEmails;

            this.Log = LoggerManager.GetLogger(this.GetType());

            try
            {
                numberOfDraftEmails = int.Parse(numberOfDraftEmailsMark.GetText());
                Log.Debug(string.Format("The number of draft emails is {0}.", numberOfDraftEmails));
            }
            catch (NoSuchElementException)
            {
                numberOfDraftEmails = 0;
                Log.Error("There are no draft emails found.");
            }

            return numberOfDraftEmails;
        }

        public string GetActualInfoMessage()
        {
            return actualInfoMessage.GetText();
        }

        public void GoToDisk()
        {
            diskIcon.Click();
        }
    }
}