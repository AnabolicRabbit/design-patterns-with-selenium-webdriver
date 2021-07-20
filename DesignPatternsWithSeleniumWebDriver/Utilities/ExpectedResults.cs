using DesignPatternsWithSeleniumWebDriver.WebObjects;

namespace DesignPatternsWithSeleniumWebDriver.Utilities
{
    public class ExpectedResults
    {
        private readonly DiskPage diskPage = new DiskPage();

        public string expectedUserName = "Selenium1Web1Driver";
        public string expectedAddressee = "Selenium WebDriver";
        public string expectedSubject = "Greeting";
        public string expectedBody = "Hi, Selenium!";
        public int expectedDraftEmailsDifference = 1;
        public string expectedLoginMessage = "Войдите с Яндекс ID, чтобы перейти к Почте";
        public string expectedDraftInfoMessage = "В папке «Черновики» нет писем.";
        public string expectedDeletedInfoMessage = "В папке «Удалённые» нет писем.";

        public string expectedDeletedPictureName => GetExpectedPictureName();

        public string GetExpectedPictureName()
        {
            return diskPage.GetPictureName();
        }
    }
}