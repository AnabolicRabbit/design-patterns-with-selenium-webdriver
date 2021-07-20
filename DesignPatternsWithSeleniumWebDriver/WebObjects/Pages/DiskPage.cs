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
        private readonly BaseElement pictureForDeletionElement = new BaseElement(By.XPath("(//div[contains(@class, 'listing-item__icon')])[2]"));
        private readonly BaseElement recycleBinElement = new BaseElement(By.XPath("(//div[contains(@class, 'listing-item__icon')])[last()]"));

        public string GetPictureName()
        {
            return pictureIcon.GetText();
        }

        public void MovePictureToRecyclerBin()
        {
            IWebElement pictureForDeletion = pictureForDeletionElement.GetElement();
            IWebElement recycleBin = recycleBinElement.GetElement();
            new Actions(Browser.Driver).DragAndDrop(pictureForDeletion, recycleBin).Build().Perform();
        }

        public void GoToRecyclerBin()
        {
            IWebElement recycleBin = recycleBinElement.GetElement();
            new Actions(Browser.Driver).DoubleClick(recycleBin).Build().Perform();
        }
    }
}