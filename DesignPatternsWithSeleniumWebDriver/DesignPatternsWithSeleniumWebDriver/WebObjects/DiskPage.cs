using DesignPatternsWithSeleniumWebDriver.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace DesignPatternsWithSeleniumWebDriver.WebObjects
{
    class DiskPage : BasePage
    {
        private static readonly By uploadButton = By.ClassName("upload-button__attach");

        public DiskPage() : base(uploadButton, "Disk Page") { }

        private readonly BaseElement pictureIcon = new BaseElement(By.XPath("(//span[@class='clamped-text'])[1]"));
        private readonly BaseElement deletedPictureIcon = new BaseElement(By.XPath("(//div/span[@class='clamped-text'])[1]"));

        private By pictureForDeletionElement = By.XPath("(//div[contains(@class, 'listing-item__icon')])[2]");
        private By recycleBinElement = By.XPath("(//div[contains(@class, 'listing-item__icon')])[last()]");

        public string GetPictureName()
        {
            return pictureIcon.GetText();
        }

        public void MovePictureToRecyclerBin()
        {
            IWebElement pictureForDeletion = Browser.Driver.FindElement(pictureForDeletionElement);
            IWebElement recycleBin = Browser.Driver.FindElement(recycleBinElement);
            new Actions(Browser.Driver).DragAndDrop(pictureForDeletion, recycleBin).Build().Perform();
        }

        public void GoToRecyclerBin()
        {
            IWebElement recycleBin = Browser.Driver.FindElement(recycleBinElement);
            new Actions(Browser.Driver).DoubleClick(recycleBin).Build().Perform();
        }

        public string GetDeletedPictureName()
        {
            return deletedPictureIcon.GetText();
        }
    }
}