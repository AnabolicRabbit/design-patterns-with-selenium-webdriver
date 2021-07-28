using DesignPatternsWithSeleniumWebDriver.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace DesignPatternsWithSeleniumWebDriver.WebObjects
{
    class DiskPage : BasePage
    {
        private static readonly By uploadButton = By.ClassName("upload-button__attach");

        public DiskPage() : base(uploadButton, "Disk Page") { }

        private readonly By listItem = By.XPath("//div[contains(@class, 'listing-item')]");
        private readonly By icon = By.XPath("//div[contains(@class, 'listing-item__icon')]");

        public string GetPictureName(string pictureItemName)
        {
            var wait = new WebDriverWait(Browser.Driver, TimeSpan.FromSeconds(5));
            IWebElement pictureIcon = wait.Until(e => e.FindElement(icon));
            string pictureName = Browser.Driver.FindElements(listItem).First(x => x.Text.Equals(pictureItemName)).Text;
            return pictureName;
        }

        public void MovePictureToRecyclerBin(string pictureItemName, string recycleBinItemName)
        {
            var actions = new Actions(Browser.Driver);
            IWebElement pictureForDeletion = Browser.Driver.FindElements(listItem).First(x => x.Text.Equals(pictureItemName));
            IWebElement recycleBin = Browser.Driver.FindElements(listItem).First(x => x.Text.Equals(recycleBinItemName));
            actions.DragAndDrop(pictureForDeletion, recycleBin).Build().Perform();
        }

        public void GoToRecyclerBin(string item)
        {
            var actions = new Actions(Browser.Driver);
            IWebElement recycleBin = Browser.Driver.FindElements(listItem).First(x => x.Text.Equals(item));
            actions.DoubleClick(recycleBin).Build().Perform();
        }
    }
}