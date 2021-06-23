using OpenQA.Selenium;
using System.Linq;
using DesignPatternsWithSeleniumWebDriver.WebDriver;

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
        private readonly BaseElement bodyField = new BaseElement(By.XPath("//div[@class='cke_wysiwyg_div cke_reset cke_enable_context_menu cke_editable cke_editable_themed cke_contents_ltr cke_htmlplaceholder']"));
        private readonly BaseElement draftsOption = new BaseElement(By.XPath("//a[@data-title='Черновики']"));
        private readonly BaseElement draftEmail = new BaseElement(By.XPath("//span[@class='mail-MessageSnippet-FromText']"));
        private readonly BaseElement sendButton = new BaseElement(By.XPath("//button[@class='control button2 button2_view_default button2_tone_default button2_size_l button2_theme_action button2_pin_circle-circle ComposeControlPanelButton-Button ComposeControlPanelButton-Button_action']"));
        private readonly BaseElement popupWindow = new BaseElement(By.XPath("//a[@class='control link link_theme_normal ComposeDoneScreen-Link']"));
        private readonly BaseElement sentOption = new BaseElement(By.XPath("//a[@data-title='Отправленные']"));
        private readonly BaseElement userLogo = new BaseElement(By.XPath("(//div/img[@class='user-pic__image'])[1]"));
        private readonly BaseElement signOutOption = new BaseElement(By.XPath("//span[text()='Выйти из сервисов Яндекса']"));
        private readonly BaseElement emailCheckbox = new BaseElement(By.XPath("(//div[@class=' js-messages-item-checkbox mail-MessageSnippet-CheckboxNb-Container']/label)[1]"));
        private readonly BaseElement deleteButton = new BaseElement(By.XPath("//span[text()='Удалить']"));
        private readonly BaseElement deletedOption = new BaseElement(By.XPath("//a[@data-title='Удалённые']"));
        private readonly BaseElement allEmailsCheckbox = new BaseElement(By.XPath("(//span[@class='checkbox_view'])[1]"));
        private readonly BaseElement clearMark = new BaseElement(By.XPath("//span[@class='mail-NestedList-Item-Clean ns-action']"));
        private readonly BaseElement clearButton = new BaseElement(By.XPath("//span[text()='Очистить']"));
        private readonly BaseElement actualUserName = new BaseElement(By.XPath("//a[@class='user-account user-account_left-name user-account_has-ticker_yes user-account_has-accent-letter_yes count-me legouser__current-account legouser__current-account i-bem']/span[1]"));
        private readonly BaseElement actualAddressee = new BaseElement(By.XPath("(//span[@class='mail-MessageSnippet-Item mail-MessageSnippet-Item_sender js-message-snippet-sender']/span)[1]"));
        private readonly BaseElement actualSubject = new BaseElement(By.XPath("(//span[@class='mail-MessageSnippet-Item mail-MessageSnippet-Item_subject']/span)[1]"));
        private readonly BaseElement actualBody = new BaseElement(By.XPath("(//span[@class='mail-MessageSnippet-Item mail-MessageSnippet-Item_firstline js-message-snippet-firstline']/span)[1]"));
        private readonly BaseElement numberOfDraftEmails = new BaseElement(By.XPath("//a[@href='#draft']/div/span"));
        private readonly BaseElement actualInfoMessage = new BaseElement(By.XPath("(//div[@class='b-messages__placeholder']/div)[1]"));

        public void SwitchToEmailPage()
        {
            var currentTab = Browser.GetDriver().WindowHandles.Last();
            Browser.GetDriver().SwitchTo().Window(currentTab);
        }

        public void CreateDraftEmail()
        {
            writeEmailButton.Click();
            toWhomField.Click();
            toWhomOption.Click();
            subjectField.SendKeys("Greeting");
            bodyField.SendKeys("Hi, Selenium!");
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

        public string GetNumberOfDraftEmails()
        {
            return numberOfDraftEmails.GetText();
        }

        public string GetActualInfoMessage()
        {
            return actualInfoMessage.GetText();
        }
    }
}