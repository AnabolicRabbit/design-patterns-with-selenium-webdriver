using DesignPatternsWithSeleniumWebDriver.WebDriver;
using NUnit.Framework;

namespace DesignPatternsWithSeleniumWebDriver
{
    public abstract class BaseTest
    {
        protected Browser browser = Browser.Instance;

        [SetUp]
        public virtual void InitializeTest()
        {
            browser = Browser.Instance;
            Browser.NavigateTo("https://yandex.by/");
            Browser.MaximizeWindow();
        }

        [TearDown]
        public virtual void CleanTest()
        {
            Browser.GetDriver().Close();
            Browser.GetDriver().Quit();
        }
    }
}