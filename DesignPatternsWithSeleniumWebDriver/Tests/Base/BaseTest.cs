using DesignPatternsWithSeleniumWebDriver.Entity;
using DesignPatternsWithSeleniumWebDriver.WebDriver;
using DesignPatternsWithSeleniumWebDriver.WebObjects;
using DesignPatternsWithSeleniumWebDriver.Logging;
using NUnit.Framework;
using System;
using System.IO;
using NUnit.Framework.Interfaces;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using DesignPatternsWithSeleniumWebDriver.Utilities.Reporting;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace DesignPatternsWithSeleniumWebDriver
{
    public abstract class BaseTest
    {
        private readonly HomePage homePage = new HomePage();
        private readonly SignInAndOutPage signInAndOutPage = new SignInAndOutPage();
        private readonly EmailPage emailPage = new EmailPage();

        public string subjectText = "Greeting";
        public string bodyText = "Hi, Selenium!";
        public string pictureItemName = "Горы.jpg";
        public string recycleBinItemName = "Корзина";

        protected Logger Log;
        private readonly string directory;
        private ExtentHtmlReporter htmlReporter;
        protected ExtentReports extent;
        protected string testName;
        protected ExtentTest test;

        public BaseTest()
        {
            this.Log = LoggerManager.GetLogger(this.GetType());
            this.directory = Path.Combine(Environment.CurrentDirectory, "YandexMailTestReporting", ReportTaker.reportFolderName);
        }

        [OneTimeSetUp]
        protected void ExtentStart()
        {
            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;

            string reportSaveFullPath = ReportTaker.TakeReport(directory);
            htmlReporter = new ExtentHtmlReporter(reportSaveFullPath);
            htmlReporter.LoadConfig(projectPath + "report-config.xml");

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Application Under Test", "Yandex Mail");
            extent.AddSystemInfo("User Name", "Selenium1Web1Driver");
        }

        [SetUp]
        public virtual void InitializeTest()
        {
            Browser.GetInstance();
            Browser.NavigateTo();
            Browser.MaximizeWindow();

            var login = Convert.ToString(Configuration.Login);
            var password = Convert.ToString(Configuration.Password);

            var user = new User(login, password);

            this.Log.Info("***Starting the test run.*** Precondition execution.");
            this.Log.Info("Opening the sign in form by clicking 'Войти' button.");
            homePage.GoToSignInForm();
            this.Log.Info("Entering the login and clicking 'Войти' button.");
            signInAndOutPage.LoginToEmail(user);
            this.Log.Info("Entering the password and clicking 'Войти' button.");
            homePage.ClickEmailButton();
            this.Log.Info("Switching to the mail box by clicking 'Почта' button.");
            emailPage.SwitchToEmailPage();
        }

        [OneTimeTearDown]
        protected void ExtentClose()
        {
            extent.Flush();
        }

        [TearDown]
        public virtual void LogAndCleanTest()
        {
            var testExecStatus = TestContext.CurrentContext.Result.Outcome.Status;

            switch (testExecStatus)
            {
                case TestStatus.Failed:
                    ScreenshotTaker.TakeScreenshot(directory, TestContext.CurrentContext.Test.Name);
                    test.Log(Status.Fail, "Fail");
                    this.Log.Warn(string.Format("***Test FAILED.*** Go to the {0} for details.", directory));
                    break;
                case TestStatus.Passed:
                    test.Log(Status.Pass, "Pass");
                    this.Log.Info("***Test  PASSED.***");
                    break;
                default:
                    break;
            }

            Browser.Driver.Close();
            Browser.Driver.Quit();
            Browser.Quit();
        }
    }
}